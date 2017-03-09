using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Scorm.Manifest
{
    public class Manifest
    {
        public string Identifier { get; set; }

        public string Version { get; set; } = "1.0";

        public SchemaVersion SchemaVersion { get; set; } = SchemaVersion.Scorm2004e4;

        public OrganizationCollection Organizations { get; } = new OrganizationCollection();

        public List<Resource> Resources { get; } = new List<Resource>();
    }
}
