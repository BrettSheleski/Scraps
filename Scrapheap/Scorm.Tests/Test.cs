using NUnit.Framework;
using System.Linq;
using System;
namespace Scorm.Tests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestCase()
		{
			var manifest = new Scorm.Manifest.Manifest
			{
				 Identifier = "Testing 123",
				Version = "1.1",

			};

			manifest.Organizations.Add(new Organization
			{
				 Identifier = "Org1",
				  Title = "My Super Duper Organizatoin"
			});

			manifest.Organizations.Default = manifest.Organizations.FirstOrDefault();

			Item item;
			for (int i = 0; i < 5; ++i)
			{
				item = new Item
				{
					Identifier = $"Item_ID{i}",
					Title = $"Item #{i}"
				};

				for (int j = 0; j < 5; ++j)
				{
					item.Items.Add(new Item
					{
						Identifier = $"SubItem_ID{i}_{j}",
						Title = $"Item #{i}-{j}"
					});
				}

				manifest.Organizations.Default.Items.Add(item);
			}

			var doc = manifest.ToXDocument();

			doc.Save("/home/brett/imsmanifest.xml",  System.Xml.Linq.SaveOptions.None);

		}
	}
}
