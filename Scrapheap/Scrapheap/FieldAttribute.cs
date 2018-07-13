using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapheap
{
    public class BindFieldToAttribute : FieldAttribute
    {
        public BindFieldToAttribute(string name)
        {
            this.FieldName = name;
        }

        public string FieldName { get; }
    }
}
