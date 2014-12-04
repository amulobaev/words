using System;

namespace Words.Api
{
    /// <summary>
    /// API definition for input plugin
    /// </summary>
    public interface IWordsInput
    {
        string Prefix { get; }

        string Description { get; }
        
        string Read(string source);
    }
}
