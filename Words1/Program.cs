using System;
using System.Collections.Generic;
using Ninject;
using Words.Api;
using Words.Common;

namespace Words1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Verify and get arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Not enough arguments");
                WaitInput();
                return;
            }
            string inputParam = args[0];
            string outputParam = args[1];

            // Initialize Ninject and resolve input/output (can be customized)
            IKernel ninjectKernel = new StandardKernel(new MyConfigModule());
            IWordsInput input = ninjectKernel.Get<IWordsInput>();
            IWordsOutput output = ninjectKernel.Get<IWordsOutput>();
            IOccurencies occurencies = ninjectKernel.Get<IOccurencies>();

            // Output to console some useful info
            Console.WriteLine("Program that counts the number of occurrences of the word.");
            Console.WriteLine("Usage: Words1.exe InputParameters OutputParameters");
            Console.WriteLine("Input: {0}", input.Description);
            Console.WriteLine("Output: {0}", output.Description);

            // Get input text
            string inputText = input.Read(inputParam);
            if (string.IsNullOrEmpty(inputText))
            {
                Console.WriteLine("Could not get input text");
                WaitInput();
                return;
            }

            // Find and count occurencies of the words
            Dictionary<string, int> words = occurencies.Find(inputText);

            // Write to output
            output.Write(outputParam, words);

            WaitInput();
        }

        static void WaitInput()
        {
            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }
    }
}
