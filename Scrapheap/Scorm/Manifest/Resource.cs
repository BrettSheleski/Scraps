using System.Collections.Generic;

namespace Scorm
{
	public class Resource
	{
		public string Identifier { get; set; }
		public ResourceType Type { get; set; } = ResourceType.WebContent;
		public ScormType ScormType { get; set; }

		public File Default { get; set; }

		public List<File> Files { get; } = new List<File>();
		public List<Resource> Dependencies { get; } = new List<Resource>();
	}
}