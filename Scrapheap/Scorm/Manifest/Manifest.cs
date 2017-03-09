using System;
using System.Xml.Linq;
using System.Linq;

namespace Scorm.Manifest
{
	public class Manifest
	{
		public string Identifier { get; set; }

		public string Version { get; set; } = "1.0";

		public SchemaVersion SchemaVersion { get; set; } = SchemaVersion.Scorm2004e4;

		public OrganizationCollection Organizations { get; } = new OrganizationCollection();

		public XDocument ToXDocument()
		{
			XNamespace ns = "http://www.imsglobal.org/xsd/imscp_v1p1";
			XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

			XDocument doc = new XDocument(
				new XDeclaration("1.0", null, "no"),
				new XElement(ns + "manifest",
							 new XElement(ns + "metadata",
										  new XElement(ns + "schema", new XText("ADL SCORM")),
										  new XElement(ns + "schemaversion", new XText("2004 3rd Edition")
							 ),
										  new XElement(ns + "organizations",
													   Organizations.Default == null ? null : new XAttribute("default", Organizations.Default.Identifier),
													   Organizations.Select(org => new XElement(ns + "organization",
																								new XAttribute("identifier", org.Identifier),
																								new XElement(ns + "title", new XText(org.Title)),
																								org.Items.Select(item =>
				                                                                                                 item.ToXElement(ns)
																												)
																		   )
													  )
										 )
				                         )
				)
			);

			return doc;
		}
	}
}
