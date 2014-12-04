using System;
using System.Collections.Generic;

namespace Words.Common
{
    /// <summary>
    /// Implementation of IOccurencies
    /// </summary>
    public class Occurencies : IOccurencies
    {
        private char[] _separators = { '.', '?', '!', ' ', ';', ':', ',' };

        /// <summary>
        /// Ctor
        /// </summary>
        public Occurencies()
        {
        }

        public char[] Separators
        {
            get { return _separators; }
            set { _separators = value; }
        }

        public Dictionary<string, int> Find(string text)
        {
            string[] allWords = text.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> words = new Dictionary<string, int>();

            foreach (string word in allWords)
            {
                if (words.ContainsKey(word))
                {
                    words[word]++;
                }
                else
                {
                    words[word] = 1;
                }
            }
            return words;
        }

    }
}
