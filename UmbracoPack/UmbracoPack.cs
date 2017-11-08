using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace UmbracoPack
{
	/// <summary>
	/// Converts NuGet package to an Umbraco package.
	/// </summary>
	/// <seealso cref="Microsoft.Build.Utilities.Task" />
	public class UmbracoPack : Task
	{
		/// <summary>
		/// Gets or sets the NuGet file to get all package information from.
		/// </summary>
		/// <value>
		/// The NuGet file.
		/// </value>
		[Required]
		public string NuspecFile { get; set; }

		/// <summary>
		/// Gets or sets the target path for the Umbraco package file (XML).
		/// </summary>
		/// <value>
		/// The Umbraco package file.
		/// </value>
		[Required]
		public string UmbPackageFile { get; set; }

		/// <summary>
		/// Gets or sets the target path for the Umbraco package (ZIP).
		/// </summary>
		/// <value>
		/// The Umbraco package target path.
		/// </value>
		/// <remarks>
		/// The file name is inferred from the <see cref="UmbPackageFile" />.
		/// </remarks>
		[Required]
		public string TargetPath { get; set; }

		/// <summary>
		/// Converts NuGet package to an Umbraco package.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the task successfully executed; otherwise, <c>false</c>.
		/// </returns>
		public override bool Execute()
		{
			// Deserialize NuGet package
			var nuGetPackage = UmbracoPack.Deserialize<Models.NuGet.Package>(this.NuspecFile);

			// Convert NuGet to Umbraco package
			var typeConverter = TypeDescriptor.GetConverter(typeof(Models.Umbraco.UmbPackage));
			if (typeConverter.CanConvertFrom(nuGetPackage.GetType()) &&
				typeConverter.ConvertFrom(nuGetPackage) is Models.Umbraco.UmbPackage umbracoPackage)
			{
				// Save Umbraco package as ZIP and XML file
				string targetFileName = Path.GetFileNameWithoutExtension(this.UmbPackageFile) + ".zip",
					targetFilePath = Path.Combine(this.TargetPath, targetFileName);
				using (var targetFileStream = File.Create(targetFilePath))
				using (var targetZipArchive = new ZipArchive(targetFileStream, ZipArchiveMode.Create))
				{
					// Copy source files to ZIP and change GUID
					foreach (var file in umbracoPackage.Files)
					{
						string guid = file.OrgPath.Replace("/", "_") + "_" + file.OrgName;
						targetZipArchive.CreateEntryFromFile(file.Guid, guid);
						file.Guid = guid;
					}

					// Serialize Umbraco package to XML
					XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[]
					{
						new XmlQualifiedName()
					});
					UmbracoPack.Serialize(this.UmbPackageFile, umbracoPackage, namespaces);

					// Add package XML
					targetZipArchive.CreateEntryFromFile(this.UmbPackageFile, "package.xml");
				}

				return true;
			}

			return false;
		}

		/// <summary>
		/// Deserializes the XML from the specified file path.
		/// </summary>
		/// <typeparam name="T">The type to deserialize to.</typeparam>
		/// <param name="filePath">The file path.</param>
		/// <returns>
		/// The deserialized type.
		/// </returns>
		private static T Deserialize<T>(string filePath)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (var reader = new System.IO.StreamReader(filePath))
			{
				return (T)serializer.Deserialize(reader);
			}
		}

		/// <summary>
		/// Serializes the type as XML to the specified file path.
		/// </summary>
		/// <typeparam name="T">The type to serialize.</typeparam>
		/// <param name="filePath">The file path.</param>
		/// <param name="value">The value.</param>
		/// <param name="namespaces">The namespaces.</param>
		private static void Serialize<T>(string filePath, T value, XmlSerializerNamespaces namespaces = null)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (var writer = new System.IO.StreamWriter(filePath))
			{
				serializer.Serialize(writer, value, namespaces);
			}
		}
	}
}
