using System;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Linq;

namespace Scorm
{
	public class Item : IItemContainer
	{
		public string Title { get; set; }

		public string Identifier { get; set; }

		public ItemCollection Items { get; } = new ItemCollection();

		public NameValueCollection Paramters { get; } = new  NameValueCollection();

		public Resource Resource { get; set; }

		internal XElement ToXElement(XNamespace ns)
		{
			return new XElement(ns + "item",
								new XAttribute("identifier", this.Identifier),
								Resource == null ? null : new XAttribute("identifierref", Resource.Identifier),
								new XElement(ns + "title", new XText(Title)),
			                    Items.Select(item => item.ToXElement(ns))
			                   );
		}
	}
}
