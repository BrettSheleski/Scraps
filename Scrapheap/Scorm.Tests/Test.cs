using NUnit.Framework;
using System.Linq;
using System;
using Scorm.Manifest;

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
                Title = "My Super Duper Organization"
            });

            manifest.Organizations.Default = manifest.Organizations.FirstOrDefault();

            Resource resource;
            File file;
            for (int i = 0; i < 10; ++i)
            {
                resource = new Resource
                {
                    Identifier = $"Resource_{i}",
                    Type = ResourceType.WebContent,
                    ScormType = ScormType.SCO
                };

                for (int j = 0; j < 10; ++j)
                {
                    file = new File
                    {
                        Href = $"path/to/file_{i}_{j}.html"
                    };
                    resource.Files.Add(file);
                }

                resource.Default = resource.Files.FirstOrDefault();
                manifest.Resources.Add(resource);
            }


            manifest.Resources.First().Dependencies.AddRange(manifest.Resources.Skip(7));

            

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
                        Title = $"Item #{i}-{j}",
                        Resource = manifest.Resources.OrderBy(x => Guid.NewGuid()).FirstOrDefault(),
                        
                    });
                }

                for (int k = 0; k < 5; ++k) {
                    item.Items.Last().Parameters.Add($"key_{i}_{k}", $"val_{i}_{k}");
                }
                
                manifest.Organizations.Default.Items.Add(item);
            }

            Scorm.Manifest.ManifestXmlWriter writer = new ManifestXmlWriter();
            
            var doc = writer.CreateDocument(manifest);

            var filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "imsmanifest.xml");
            doc.Save(filePath, System.Xml.Linq.SaveOptions.None);

        }
    }
}
