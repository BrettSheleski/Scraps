using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapheap
{
    public static class AlliterationDetector
    {
        private static readonly string[] wordsToIgnore = new string[] {"I", "A", "ABOUT", "AN", "AND", "ARE", "AS", "AT", "BE",
        "BY", "COM", "FOR", "FROM", "HOW", "IN", "IS", "IT",
        "OF", "ON", "OR", "THAT", "THE", "THIS", "TO", "WAS",
        "WHAT", "WHEN", "WHERE", "WHO", "WILL", "WITH"};

        public static bool IsAlliteration(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                throw new ArgumentException();

            int numberOfStartingCharacters = phrase.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                                   .Select(word => word.ToUpper())
                                                   .Where(word => !wordsToIgnore.Contains(word))
                                                   .GroupBy(word => word[0])
                                                   .Count();

            return numberOfStartingCharacters == 1;
        }
    }
}
