using System.Collections.Generic;

namespace Words.Common
{
    /// <summary>
    /// Interface of class for finding occurencies of the words
    /// </summary>
    public interface IOccurencies
    {
        Dictionary<string, int> Find(string text);
    }
}