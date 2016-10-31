using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapheap.Extensions
{
    public static class StringExtensions
    {
        public static bool IsAlliteration(this string phrase)
        {
            return AlliterationDetector.IsAlliteration(phrase);
        }
    }
}
