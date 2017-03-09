using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Scorm.Manifest
{
    public class ManifestXmlWriter
    {
        public XDocument CreateDocument(Manifest manifest)
        {
            XNamespace ns = "http://www.imsglobal.org/xsd/imscp_v1p1";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace adlcp = "http://www.adlnet.org/xsd/adlcp_v1p3";
            XNamespace adlseq = "http://www.adlnet.org/xsd/adlseq_v1p3";
            XNamespace adlnav = "http://www.adlnet.org/xsd/adlnav_v1p3";
            XNamespace imsss = "http://www.imsglobal.org/xsd/imsss";
            string schemaLocation = @"http://www.imsglobal.org/xsd/imscp_v1p1 imscp_v1p1.xsd http://www.adlnet.org/xsd/adlcp_v1p3 adlcp_v1p3.xsd http://www.adlnet.org/xsd/adlseq_v1p3 adlseq_v1p3.xsd http://www.adlnet.org/xsd/adlnav_v1p3 adlnav_v1p3.xsd http://www.imsglobal.org/xsd/imsss imsss_v1p0.xsd";

            XDocument doc = new XDocument(
                new XDeclaration("1.0", null, "no"),
                new XElement(ns + "manifest",
                            new XAttribute("identifier", manifest.Identifier),
                            new XAttribute("version", manifest.Version),
                            new XAttribute("xmlns", ns.NamespaceName),
                            new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                            new XAttribute(XNamespace.Xmlns + "adlcp", adlcp),
                            new XAttribute(XNamespace.Xmlns + "adlseq", adlseq),
                            new XAttribute(XNamespace.Xmlns + "adlnav", adlnav),
                            new XAttribute(XNamespace.Xmlns + "imsss", imsss),
                             //new XAttribute(xsi + "schemaLocation", schemaLocation), 
                             new XElement(ns + "metadata",
                                          new XElement(ns + "schema", new XText("ADL SCORM")),
                                          new XElement(ns + "schemaversion", new XText("2004 3rd Edition"))
                                          ),
                                          new XElement(ns + "organizations",
                                                       manifest.Organizations.Default == null ? null : new XAttribute("default", manifest.Organizations.Default.Identifier),
                                                       manifest.Organizations.Select(org => new XElement(ns + "organization",
                                                                                                new XAttribute("identifier", org.Identifier),
                                                                                                new XElement(ns + "title", new XText(org.Title)),
                                                                                                org.Items.Select(item =>ToXElement(item, ns))
                                                                           )
                                                      )
                                         ),
                                          new XElement(ns + "resources",
                                            manifest.Resources.Select(r => new XElement(ns + "resource",
                                            new XAttribute("identifier", r.Identifier),
                                            new XAttribute("type", r.Type),
                                            new XAttribute(adlcp + "scormType", r.ScormType),
                                            new XAttribute("href", r.Default?.Href),
                                            r.Files.Select(f => new XElement(ns + "file", new XAttribute("href", f.Href))),
                                            r.Dependencies.Select(d => new XElement(ns + "dependency", new XAttribute("identifierref", d.Identifier)))
                                          )
                                         )))
            );

            return doc;
        }


        public void WriteTo(Manifest manifest, System.IO.Stream stream)
        {
            CreateDocument(manifest).Save(stream);
        }

        public void WriteTo(Manifest manifest, System.IO.TextWriter writer)
        {
            CreateDocument(manifest).Save(writer);
        }

        protected XElement ToXElement(Item item, XNamespace ns)
        {
            return new XElement(ns + "item",
                                new XAttribute("identifier", item.Identifier),
                                item.Resource == null ? null : new XAttribute("identifierref", item.Resource.Identifier),
                                item.Parameters.Count == 0 ? null : new XAttribute("parameters", "?" + string.Join("&", item.Parameters.AllKeys.Select(key => $"{System.Web.HttpUtility.UrlEncode(key)}={System.Web.HttpUtility.UrlEncode(item.Parameters[key])}"))),
                                new XElement(ns + "title", new XText(item.Title)),
                                item.Items.Select(i => ToXElement(i, ns))
                               );
        }
    }
}
