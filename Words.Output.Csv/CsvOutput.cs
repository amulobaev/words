using System;
using System.Collections.Generic;
using System.IO;
using Words.Api;

namespace Words.Output.Csv
{
    /// <summary>
    /// IWordsOutput implementation for CSV output
    /// </summary>
    public class CsvOutput : IWordsOutput
    {
        public string Prefix
        {
            get { return "csv"; }
        }

        public string Description
        {
            get { return "output to CSV file, specify filename as a parameter"; }
        }

        public void Write(string destination, Dictionary<string, int> data)
        {
            try
            {
                using (StreamWriter writer = File.CreateText(destination))
                {
                    foreach (KeyValuePair<string, int> item in data)
                    {
                        writer.WriteLine("{0}, {1}", item.Key, item.Value);
                    }
                }

                Console.WriteLine("Data exported to CSV");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file {0} could not be wriiten", destination);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
