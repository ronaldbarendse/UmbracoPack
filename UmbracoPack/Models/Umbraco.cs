using System;
using System.ComponentModel;
using System.Xml.Serialization;
using UmbracoPack.TypeConverters;

namespace UmbracoPack.Models.Umbraco
{
	[Serializable]
	[TypeConverter(typeof(UmbPackageConverter))]
	[XmlRoot("umbPackage", Namespace = "", IsNullable = false)]
	public class UmbPackage
	{
		[XmlElement("info")]
		public UmbPackageInfo Info { get; set; }

		[XmlArray("macros")]
		[XmlArrayItem("macro", IsNullable = false)]
		public UmbPackageMacro[] Macros { get; set; }

		[XmlArray("files")]
		[XmlArrayItem("file", IsNullable = false)]
		public UmbPackageFile[] Files { get; set; }

		[XmlElement("commands")]
		public string Commands { get; set; }

		[XmlElement("control")]
		public string Control { get; set; }

		[XmlArray("DocumentTypes")]
		[XmlArrayItem("DocumentType", IsNullable = false)]
		public UmbPackageDocumentType[] DocumentTypes { get; set; }

		[XmlArray("Templates")]
		[XmlArrayItem("Template", IsNullable = false)]
		public UmbPackageTemplate[] Templates { get; set; }

		[XmlArray("Stylesheets")]
		[XmlArrayItem("Stylesheet", IsNullable = false)]
		public UmbPackageStylesheet[] Stylesheets { get; set; }

		[XmlElement("Documents")]
		public UmbPackageDocuments Documents { get; set; }
	}

	[Serializable]
	public class UmbPackageInfo
	{
		[XmlElement("package")]
		public UmbPackageInfoPackage Package { get; set; }

		[XmlElement("author")]
		public UmbPackageInfoAuthor Author { get; set; }

		[XmlElement("readme")]
		public string Readme { get; set; }

		[XmlElement("documentation")]
		public UmbPackageInfoDocumentation Documentation { get; set; }
	}

	[Serializable]
	public class UmbPackageInfoPackage
	{
		[XmlElement("name")]
		public string Name { get; set; }

		[XmlElement("uniqueId")]
		public string UniqueId { get; set; }

		[XmlElement("version")]
		public string Version { get; set; }

		[XmlElement("license")]
		public UmbPackageInfoPackageLicense License { get; set; }

		[XmlElement("url")]
		public string Url { get; set; }

		[XmlElement("requirements")]
		public UmbPackageInfoPackageRequirements Requirements { get; set; }

		[XmlElement("upgradeBehavior")]
		public UpgradeBehaviorType UpgradeBehavior { get; set; }
	}

	[Serializable]
	public class UmbPackageInfoPackageLicense
	{
		[XmlAttribute("url")]
		public string Url { get; set; }

		[XmlText]
		public string Name { get; set; }

		public UmbPackageInfoPackageLicense()
		{ }

		public UmbPackageInfoPackageLicense(string url)
		{
			this.Url = url ?? "http://opensource.org/licenses/MIT";
			this.Name = this.Url.Substring(this.Url.LastIndexOf('/') + 1);
		}
	}

	[Serializable]
	public class UmbPackageInfoPackageRequirements
	{
		[XmlElement("major")]
		public byte Major { get; set; }

		[XmlElement("minor")]
		public byte Minor { get; set; }

		[XmlElement("patch")]
		public byte Patch { get; set; }

		[XmlArray("packages")]
		[XmlArrayItem("package", IsNullable = false)]
		public UmbPackageInfoPackageRequirementsPackage[] Packages { get; set; }
	}

	[Serializable]
	public class UmbPackageInfoPackageRequirementsPackage
	{
		[XmlAttribute("guid")]
		public string Guid { get; set; }

		[XmlAttribute("versionMajor")]
		public byte VersionMajor { get; set; }

		[XmlAttribute("versionMinor")]
		public byte VersionMinor { get; set; }

		[XmlAttribute("versionPatch")]
		public byte VersionPatch { get; set; }
	}

	[Serializable]
	public enum UpgradeBehaviorType
	{
		[XmlEnum("overwrite")]
		Overwrite,
		[XmlEnum("duplicate")]
		Duplicate,
		[XmlEnum("abort")]
		Abort
	}

	[Serializable]
	public class UmbPackageInfoAuthor
	{
		[XmlElement("name")]
		public string Name { get; set; }

		[XmlElement("website")]
		public string Website { get; set; }
	}

	[Serializable]
	public class UmbPackageInfoDocumentation
	{
		[XmlElement("releaseNotes")]
		public string ReleaseNotes { get; set; }

		[XmlElement("installation")]
		public string Installation { get; set; }

		[XmlElement("gettingStarted")]
		public string GettingStarted { get; set; }
	}

	[Serializable]
	public class UmbPackageMacro
	{
		[XmlElement("name")]
		public string Name { get; set; }

		[XmlElement("alias")]
		public string Alias { get; set; }

		[XmlElement("scriptType")]
		public object ScriptType { get; set; }

		[XmlElement("scriptAssembly")]
		public object ScriptAssembly { get; set; }

		[XmlElement("xslt")]
		public string Xslt { get; set; }

		[XmlElement("useInEditor")]
		public bool UseInEditor { get; set; }

		[XmlElement("refreshRate")]
		public byte RefreshRate { get; set; }

		[XmlArray("properties")]
		[XmlArrayItem("property", IsNullable = false)]
		public UmbPackageMacroProperty[] Properties { get; set; }
	}

	[Serializable]
	public class UmbPackageMacroProperty
	{
		[XmlAttribute("show")]
		public bool Show { get; set; }

		[XmlAttribute("propertyType")]
		public string PropertyType { get; set; }

		[XmlAttribute("alias")]
		public string Alias { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }
	}

	[Serializable]
	public class UmbPackageFile
	{
		[XmlElement("guid")]
		public string Guid { get; set; }

		[XmlElement("orgPath")]
		public string OrgPath { get; set; }

		[XmlElement("orgName")]
		public string OrgName { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentType
	{
		[XmlElement("Info")]
		public UmbPackageDocumentTypeInfo Info { get; set; }

		[XmlElement("Structure")]
		public UmbPackageDocumentTypeStructure Structure { get; set; }

		[XmlArray("Tabs")]
		[XmlArrayItem("Tab", IsNullable = false)]
		public UmbPackageDocumentTypeTab[] Tabs { get; set; }

		[XmlArray("GenericProperties")]
		[XmlArrayItem("GenericProperty", IsNullable = false)]
		public UmbPackageDocumentTypeGenericProperty[] GenericProperties { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentTypeInfo
	{
		[XmlElement("Name")]
		public string Name { get; set; }

		[XmlElement("Alias")]
		public string Alias { get; set; }

		[XmlElement("Icon")]
		public string Icon { get; set; }

		[XmlElement("Thumbnail")]
		public string Thumbnail { get; set; }

		[XmlElement("Description")]
		public string Description { get; set; }

		[XmlElement("AllowedTemplates")]
		public UmbPackageDocumentTypeInfoAllowedTemplates AllowedTemplates { get; set; }

		[XmlElement("DefaultTemplate")]
		public string DefaultTemplate { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentTypeInfoAllowedTemplates
	{
		[XmlElement("Template")]
		public string Template { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentTypeStructure
	{
		[XmlElement("DocumentType")]
		public string DocumentType { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentTypeTab
	{
		[XmlElement("Id")]
		public byte Id { get; set; }

		[XmlElement("Caption")]
		public string Caption { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentTypeGenericProperty
	{
		[XmlElement("Name")]
		public string Name { get; set; }

		[XmlElement("Alias")]
		public string Alias { get; set; }

		[XmlElement("Type")]
		public string Type { get; set; }

		[XmlElement("Tab")]
		public string Tab { get; set; }

		[XmlElement("Mandatory")]
		public string Mandatory { get; set; }

		[XmlElement("Validation")]
		public object Validation { get; set; }

		[XmlElement("Description")]
		public string Description { get; set; }
	}

	[Serializable]
	public class UmbPackageTemplate
	{
		[XmlElement("Name")]
		public string Name { get; set; }

		[XmlElement("Alias")]
		public string Alias { get; set; }

		[XmlElement("Master")]
		public string Master { get; set; }

		[XmlElement("Design")]
		public string Design { get; set; }
	}

	[Serializable]
	public class UmbPackageStylesheet
	{
		[XmlElement("Name")]
		public string Name { get; set; }

		[XmlElement("FileName")]
		public object FileName { get; set; }

		[XmlElement("Content")]
		public string Content { get; set; }

		[XmlArray("Properties")]
		[XmlArrayItem("Property", IsNullable = false)]
		public UmbPackageStylesheetProperty[] Properties { get; set; }
	}

	[Serializable]
	public class UmbPackageStylesheetProperty
	{
		[XmlElement("Name")]
		public string Name { get; set; }

		[XmlElement("Alias")]
		public string Alias { get; set; }

		[XmlElement("Value")]
		public string Value { get; set; }
	}

	[Serializable]
	public class UmbPackageDocuments
	{
		[XmlElement("DocumentSet")]
		public UmbPackageDocumentsDocumentSet DocumentSet { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentsDocumentSet
	{
		[XmlElement("node")]
		public UmbPackageDocumentsDocumentSetNode Node { get; set; }

		[XmlAttribute("importMode")]
		public ImportModeType ImportMode { get; set; }

		[XmlAttribute("parentId")]
		public byte ParentId { get; set; }

		[XmlIgnore]
		public bool ParentIdSpecified => this.ParentId != 0;
	}

	[Serializable]
	public class UmbPackageDocumentsDocumentSetNode
	{
		[XmlElement("data")]
		public UmbPackageDocumentsDocumentSetNodeData[] Data { get; set; }

		[XmlElement("node")]
		public UmbPackageDocumentsDocumentSetNodeNode[] Node { get; set; }

		[XmlAttribute("id")]
		public ushort Id { get; set; }

		[XmlAttribute("version")]
		public string Version { get; set; }

		[XmlAttribute("parentID")]
		public sbyte ParentId { get; set; }

		[XmlAttribute("level")]
		public byte Level { get; set; }

		[XmlAttribute("writerID")]
		public byte WriterId { get; set; }

		[XmlAttribute("creatorID")]
		public byte CreatorId { get; set; }

		[XmlAttribute("nodeType")]
		public ushort NodeType { get; set; }

		[XmlAttribute("template")]
		public ushort Template { get; set; }

		[XmlAttribute("sortOrder")]
		public byte SortOrder { get; set; }

		[XmlAttribute("createDate")]
		public DateTime CreateDate { get; set; }

		[XmlAttribute("updateDate")]
		public DateTime UpdateDate { get; set; }

		[XmlAttribute("nodeName")]
		public string NodeName { get; set; }

		[XmlAttribute("urlName")]
		public string UrlName { get; set; }

		[XmlAttribute("writerName")]
		public string WriterName { get; set; }

		[XmlAttribute("creatorName")]
		public string CreatorName { get; set; }

		[XmlAttribute("nodeTypeAlias")]
		public string NodeTypeAlias { get; set; }

		[XmlAttribute("path")]
		public string Path { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentsDocumentSetNodeData
	{
		[XmlAttribute("versionID")]
		public string VersionId { get; set; }

		[XmlAttribute("alias")]
		public string Alias { get; set; }

		[XmlText]
		public string Value { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentsDocumentSetNodeNode
	{
		[XmlElement("data")]
		public UmbPackageDocumentsDocumentSetNodeNodeData[] Data { get; set; }

		[XmlElement("node")]
		public UmbPackageDocumentsDocumentSetNodeNodeNode[] Node { get; set; }

		[XmlAttribute("id")]
		public ushort Id { get; set; }

		[XmlAttribute("version")]
		public string Version { get; set; }

		[XmlAttribute("parentID")]
		public ushort ParentId { get; set; }

		[XmlAttribute("level")]
		public byte Level { get; set; }

		[XmlAttribute("writerID")]
		public byte WriterId { get; set; }

		[XmlAttribute("creatorID")]
		public byte CreatorId { get; set; }

		[XmlAttribute("nodeType")]
		public ushort NodeType { get; set; }

		[XmlAttribute("template")]
		public ushort Template { get; set; }

		[XmlAttribute("sortOrder")]
		public byte SortOrder { get; set; }

		[XmlAttribute("createDate")]
		public DateTime CreateDate { get; set; }

		[XmlAttribute("updateDate")]
		public DateTime UpdateDate { get; set; }

		[XmlAttribute("nodeName")]
		public string NodeName { get; set; }

		[XmlAttribute("urlName")]
		public string UrlName { get; set; }

		[XmlAttribute("writerName")]
		public string WriterName { get; set; }

		[XmlAttribute("creatorName")]
		public string CreatorName { get; set; }

		[XmlAttribute("nodeTypeAlias")]
		public string NodeTypeAlias { get; set; }

		[XmlAttribute("path")]
		public string Path { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentsDocumentSetNodeNodeData
	{
		[XmlAttribute("versionID")]
		public string VersionId { get; set; }

		[XmlAttribute("alias")]
		public string Alias { get; set; }

		[XmlText]
		public string Value { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentsDocumentSetNodeNodeNode
	{
		[XmlElement("data")]
		public UmbPackageDocumentsDocumentSetNodeNodeNodeData[] Data { get; set; }

		[XmlAttribute("id")]
		public ushort Id { get; set; }

		[XmlAttribute("version")]
		public string Version { get; set; }

		[XmlAttribute("parentID")]
		public ushort ParentId { get; set; }

		[XmlAttribute("level")]
		public byte Level { get; set; }

		[XmlAttribute("writerID")]
		public byte WriterId { get; set; }

		[XmlAttribute("creatorID")]
		public byte CreatorId { get; set; }

		[XmlAttribute("nodeType")]
		public ushort NodeType { get; set; }

		[XmlAttribute("template")]
		public ushort Template { get; set; }

		[XmlAttribute("sortOrder")]
		public byte SortOrder { get; set; }

		[XmlAttribute("createDate")]
		public DateTime CreateDate { get; set; }

		[XmlAttribute("updateDate")]
		public DateTime UpdateDate { get; set; }

		[XmlAttribute("nodeName")]
		public string NodeName { get; set; }

		[XmlAttribute("urlName")]
		public string UrlName { get; set; }

		[XmlAttribute("writerName")]
		public string WriterName { get; set; }

		[XmlAttribute("creatorName")]
		public string CreatorName { get; set; }

		[XmlAttribute("nodeTypeAlias")]
		public string NodeTypeAlias { get; set; }

		[XmlAttribute("path")]
		public string Path { get; set; }
	}

	[Serializable]
	public class UmbPackageDocumentsDocumentSetNodeNodeNodeData
	{
		[XmlAttribute("versionID")]
		public string VersionId { get; set; }

		[XmlAttribute("alias")]
		public string Alias { get; set; }

		[XmlText]
		public string Value { get; set; }
	}

	[Serializable]
	public enum ImportModeType
	{
		[XmlEnum("root")]
		Root,
		[XmlEnum("specified")]
		Specified,
		[XmlEnum("ask")]
		Ask
	}
}