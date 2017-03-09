using System;
namespace Scorm.Manifest
{
	public class Organization : IItemContainer
	{
		public string Title { get; set; }

		public string Identifier { get; set; }

		public ItemCollection Items { get; } = new ItemCollection();
	}
}
