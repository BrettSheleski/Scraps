using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Scorm
{
	public class OrganizationCollection : List<Organization>
	{
		public Organization Default { get; set; }

	}
}
