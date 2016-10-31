using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapheap
{
    public static class AlliterationDetector
    {
        private static string[] wordsToIgnore = new string[] {"I", "A", "ABOUT", "AN", "AND", "ARE", "AS", "AT", "BE",
        "BY", "COM", "FOR", "FROM", "HOW", "IN", "IS", "IT",
        "OF", "ON", "OR", "THAT", "THE", "THIS", "TO", "WAS",
        "WHAT", "WHEN", "WHERE", "WHO", "WILL", "WITH"};

        public static bool IsAlliteration(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                throw new ArgumentException("phrase must not be a null or empty string.");

            string[] words = phrase.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            char? firstCharacter = null;

            string upperCaseWord;

            foreach (var word in words)
            {
                upperCaseWord = word.ToUpper();

                if (!wordsToIgnore.Contains(upperCaseWord))
                {
                    if (firstCharacter == null)
                    {
                        // this is the first candidate word, so use its first letter
                        firstCharacter = upperCaseWord[0];
                    }
                    else if (word.ToUpper()[0] != firstCharacter.Value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
