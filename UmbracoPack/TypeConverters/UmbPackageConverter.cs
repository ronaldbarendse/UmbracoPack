using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UmbracoPack.TypeConverters
{
	/// <summary>
	/// Provides a way to converting a NuGet package to Umbraco package format.
	/// </summary>
	/// <seealso cref="System.ComponentModel.TypeConverter" />
	public class UmbPackageConverter : TypeConverter
	{
		/// <summary>
		/// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
		/// </summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		/// <returns>
		/// true if this converter can perform the conversion; otherwise, false.
		/// </returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(Models.NuGet.Package))
			{
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// Converts the given object to the type of this converter, using the specified context and culture information.
		/// </summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>
		/// An <see cref="T:System.Object" /> that represents the converted value.
		/// </returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is Models.NuGet.Package nuGetPackage)
			{
				var umbPackage = new Models.Umbraco.UmbPackage()
				{
					Info = new Models.Umbraco.UmbPackageInfo()
					{
						Package = new Models.Umbraco.UmbPackageInfoPackage()
						{
							Name = nuGetPackage.Metadata.Title ?? nuGetPackage.Metadata.Id,
							Version = nuGetPackage.Metadata.Version,
							License = new Models.Umbraco.UmbPackageInfoPackageLicense(nuGetPackage.Metadata.LicenseUrl),
							Url = nuGetPackage.Metadata.ProjectUrl ?? "https://our.umbraco.org/",
							Requirements = this.GetRequirements(nuGetPackage)
						},
						Author = new Models.Umbraco.UmbPackageInfoAuthor()
						{
							Name = nuGetPackage.Metadata.Authors,
							Website = nuGetPackage.Metadata.ProjectUrl ?? "https://our.umbraco.org/"
						},
						Readme = nuGetPackage.Metadata.Description ?? nuGetPackage.Metadata.Summary,
						Documentation = new Models.Umbraco.UmbPackageInfoDocumentation()
						{
							ReleaseNotes = nuGetPackage.Metadata.ReleaseNotes
						}
					},
					Files = this.GetFiles(nuGetPackage).ToArray()
				};

				return umbPackage;
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// Gets the requirements.
		/// </summary>
		/// <param name="nuGetPackage">The NuGet package.</param>
		/// <returns>
		/// The requirements.
		/// </returns>
		protected Models.Umbraco.UmbPackageInfoPackageRequirements GetRequirements(Models.NuGet.Package nuGetPackage)
		{
			var dependencies = nuGetPackage.Metadata.Dependencies.Items.OfType<Models.NuGet.DependencyGroup>().SelectMany(dg => dg.Dependency)
				.Union(nuGetPackage.Metadata.Dependencies.Items.OfType<Models.NuGet.Dependency>());

			var umbracoDependency = dependencies.FirstOrDefault(d => d.Id == "UmbracoCms") ?? dependencies.FirstOrDefault(d => d.Id == "UmbracoCms.Core");
			if (umbracoDependency != null && Version.TryParse(umbracoDependency.Version, out Version umbracoVersion))
			{
				return new Models.Umbraco.UmbPackageInfoPackageRequirements()
				{
					Major = (byte)umbracoVersion.Major,
					Minor = (byte)umbracoVersion.Minor,
					Patch = (byte)umbracoVersion.Build
				};
			}

			return null;
		}

		/// <summary>
		/// Gets the files.
		/// </summary>
		/// <param name="nuGetPackage">The NuGet package.</param>
		/// <returns>
		/// The files.
		/// </returns>
		protected IEnumerable<Models.Umbraco.UmbPackageFile> GetFiles(Models.NuGet.Package nuGetPackage)
		{
			// Return library (bin) files with framework preference
			var libFrameworkFiles = nuGetPackage.Files.Where(f => f.Target.StartsWith("lib\\")).GroupBy(f => f.Target.Split('\\')[1]).ToDictionary(fg => fg.Key, fg => fg.AsEnumerable());
			foreach (var framework in new[] { "net45", "netstandard1.1", "netstandard1.0", "net403", "net40", "net35", "net20", "net11" })
			{
				if (libFrameworkFiles.TryGetValue(framework, out IEnumerable<Models.NuGet.PackageFile> libFiles))
				{
					foreach (var libFile in libFiles)
					{
						var target = "bin" + libFile.Target.Substring(("lib\\" + framework).Length);

						yield return new Models.Umbraco.UmbPackageFile()
						{
							Guid = libFile.Src,
							OrgPath = Path.GetDirectoryName(target).Replace("\\", "/"),
							OrgName = Path.GetFileName(target)
						};
					}

					break;
				}
			}

			// Return content files
			var contentFiles = nuGetPackage.Files.Where(f => f.Target.StartsWith("content\\"));
			foreach (var contentFile in contentFiles)
			{
				var target = contentFile.Target.Substring("content\\".Length);

				yield return new Models.Umbraco.UmbPackageFile()
				{
					Guid = contentFile.Src,
					OrgPath = Path.GetDirectoryName(target).Replace("\\", "/"),
					OrgName = Path.GetFileName(target)
				};
			}
		}
	}
}
