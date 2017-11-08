using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace UmbracoPack.Models.NuGet
{
	[Serializable]
	[XmlRoot("package", Namespace = "http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd", IsNullable = false)]
	public class Package
	{
		[XmlElement("metadata")]
		public PackageMetadata Metadata { get; set; }

		[XmlArray("files", IsNullable = true)]
		[XmlArrayItem("file", IsNullable = false)]
		public PackageFile[] Files { get; set; }
	}

	[Serializable]
	public class PackageMetadata
	{
		public PackageMetadata()
		{
			this.Language = "en-US";
		}

		[XmlElement("id")]
		public string Id { get; set; }

		[XmlElement("version")]
		public string Version { get; set; }

		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement("authors")]
		public string Authors { get; set; }

		[XmlElement("owners")]
		public string Owners { get; set; }

		[XmlElement("licenseUrl", DataType = "anyURI")]
		public string LicenseUrl { get; set; }

		[XmlElement("projectUrl", DataType = "anyURI")]
		public string ProjectUrl { get; set; }

		[XmlElement("iconUrl", DataType = "anyURI")]
		public string IconUrl { get; set; }

		[XmlElement("requireLicenseAcceptance")]
		public bool RequireLicenseAcceptance { get; set; }

		[XmlIgnore]
		public bool RequireLicenseAcceptanceSpecified => this.RequireLicenseAcceptance;

		[XmlElement("developmentDependency")]
		public bool DevelopmentDependency { get; set; }

		[XmlIgnore]
		public bool DevelopmentDependencySpecified => this.DevelopmentDependency;

		[XmlElement("description")]
		public string Description { get; set; }

		[XmlElement("summary")]
		public string Summary { get; set; }

		[XmlElement("releaseNotes")]
		public string ReleaseNotes { get; set; }

		[XmlElement("copyright")]
		public string Copyright { get; set; }

		[DefaultValue("en-US")]
		[XmlElement("language")]
		public string Language { get; set; }

		[XmlElement("tags")]
		public string Tags { get; set; }

		[XmlElement("serviceable")]
		public bool Serviceable { get; set; }

		[XmlIgnore]
		public bool ServiceableSpecified => this.Serviceable;

		[XmlElement("repository")]
		public PackageMetadataRepository Repository { get; set; }

		[XmlArray("packageTypes")]
		[XmlArrayItem("packageType", IsNullable = false)]
		public PackageMetadataPackageType[] PackageTypes { get; set; }

		[XmlElement("dependencies")]
		public PackageMetadataDependencies Dependencies { get; set; }

		[XmlArray("frameworkAssemblies")]
		[XmlArrayItem("frameworkAssembly", IsNullable = false)]
		public PackageMetadataFrameworkAssembly[] FrameworkAssemblies { get; set; }

		[XmlElement("references")]
		public PackageMetadataReferences References { get; set; }

		[XmlElement("contentFiles")]
		public PackageMetadataContentFiles ContentFiles { get; set; }

		[XmlAttribute("minClientVersion")]
		public string MinClientVersion { get; set; }
	}

	[Serializable]
	public class PackageMetadataRepository
	{
		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("url", DataType = "anyURI")]
		public string Url { get; set; }
	}

	[Serializable]
	public class ContentFileEntries
	{
		[XmlAttribute("include")]
		public string Include { get; set; }

		[XmlAttribute("exclude")]
		public string Exclude { get; set; }

		[XmlAttribute("buildAction")]
		public string BuildAction { get; set; }

		[XmlAttribute("copyToOutput")]
		public bool CopyToOutput { get; set; }

		[XmlIgnore]
		public bool CopyToOutputSpecified => this.CopyToOutput;

		[XmlAttribute("flatten")]
		public bool Flatten { get; set; }

		[XmlIgnore]
		public bool FlattenSpecified => this.Flatten;
	}

	[Serializable]
	public class ReferenceGroup
	{
		[XmlElement("reference")]
		public Reference[] Reference { get; set; }

		[XmlAttribute("targetFramework")]
		public string TargetFramework { get; set; }
	}

	[Serializable]
	public class Reference
	{
		[XmlAttribute("file")]
		public string File { get; set; }
	}

	[Serializable]
	public class DependencyGroup
	{
		[XmlElement("dependency")]
		public Dependency[] Dependency { get; set; }

		[XmlAttribute("targetFramework")]
		public string TargetFramework { get; set; }
	}

	[Serializable]
	public class Dependency
	{
		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlAttribute("version")]
		public string Version { get; set; }

		[XmlAttribute("include")]
		public string Include { get; set; }

		[XmlAttribute("exclude")]
		public string Exclude { get; set; }
	}

	[Serializable]
	public class PackageMetadataPackageType
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("version")]
		public string Version { get; set; }
	}

	[Serializable]
	public class PackageMetadataDependencies
	{
		[XmlElement("dependency", typeof(Dependency))]
		[XmlElement("group", typeof(DependencyGroup))]
		public object[] Items { get; set; }
	}

	[Serializable]
	public class PackageMetadataFrameworkAssembly
	{
		[XmlAttribute("assemblyName")]
		public string AssemblyName { get; set; }

		[XmlAttribute("targetFramework")]
		public string TargetFramework { get; set; }
	}

	[Serializable]
	public class PackageMetadataReferences
	{
		[XmlElement("group", typeof(ReferenceGroup))]
		[XmlElement("reference", typeof(Reference))]
		public object[] Items { get; set; }
	}

	[Serializable]
	public class PackageMetadataContentFiles
	{
		[XmlElement("files")]
		public ContentFileEntries[] Items { get; set; }
	}

	[Serializable]
	public class PackageFile
	{
		[XmlAttribute("src")]
		public string Src { get; set; }

		[XmlAttribute("target")]
		public string Target { get; set; }

		[XmlAttribute("exclude")]
		public string Exclude { get; set; }
	}
}