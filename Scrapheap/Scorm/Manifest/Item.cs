using System;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Linq;

namespace Scorm.Manifest
{
	public class Item : IItemContainer
	{
		public string Title { get; set; }

		public string Identifier { get; set; }

		public ItemCollection Items { get; } = new ItemCollection();

		public NameValueCollection Paramters { get; } = new  NameValueCollection();

		public Resource Resource { get; set; }
	}
}
