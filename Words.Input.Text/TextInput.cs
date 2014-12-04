using System;
using System.IO;
using Words.Api;

namespace Words.Input.Text
{
    /// <summary>
    /// IWordsInput implementation for TEXT input
    /// </summary>
    public class TextInput : IWordsInput
    {
        public string Description
        {
            get { return "input from text file, specify file path as parameter"; }
        }

        public string Prefix
        {
            get { return "text"; }
        }

        public string Read(string source)
        {
            if (!File.Exists(source))
            {
                Console.WriteLine("File {0} does not exists", source);
                return null;
            }

            try
            {
                using (StreamReader streamReader = new StreamReader(source))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file {0} could not be read", source);
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
