using System.Collections.Generic;

namespace Words.Api
{
    /// <summary>
    /// API definition for output plugin
    /// </summary>
    public interface IWordsOutput
    {
        string Prefix { get; }
        
        string Description { get; }
        
        void Write(string destination, Dictionary<string, int> data);
    }
}
