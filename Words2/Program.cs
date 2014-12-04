using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Words.Api;
using Words.Common;

namespace Words2
{
    class Program
    {
        // Extensions collections
        private static readonly List<IWordsInput> InputExtensions = new List<IWordsInput>();
        private static readonly List<IWordsOutput> OutputExtensions = new List<IWordsOutput>();

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

            // Output to console some useful info
            Console.WriteLine("Program that counts the number of occurrences of the word.");
            Console.WriteLine("Usage: Words1.exe InputParameters OutputParameters");

            // Load extensions
            LoadExtensions();

            // Get input data
            string inputText = ParseInputParam(inputParam);

            // Find and count occurancies
            IOccurencies occurencies = new Occurencies();
            Dictionary<string, int> data = occurencies.Find(inputText);

            // Output result
            ParseOutputParams(outputParam, data);

            WaitInput();
        }

        private static string ParseInputParam(string s)
        {
            int index = s.IndexOf(":");
            string prefix = s.Substring(0, index);
            string source = s.Substring(index + 1, s.Length - index - 1);
            IWordsInput extension = InputExtensions.FirstOrDefault(x => x.Prefix == prefix);
            if (extension == null)
                return null;
            return extension.Read(source);
        }

        private static void ParseOutputParams(string s, Dictionary<string, int> data)
        {
            int index = s.IndexOf(":");
            string prefix = s.Substring(0, index);
            string source = s.Substring(index + 1, s.Length - index - 1);
            IWordsOutput extension = OutputExtensions.FirstOrDefault(x => x.Prefix == prefix);
            if (extension == null)
                return;
            extension.Write(source, data);
        }

        static void WaitInput()
        {
            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }

        /// <summary>
        /// Get files in path by pattern
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        static string[] GetFiles(string path, string pattern)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            var fileInfos = directoryInfo.GetFiles(pattern);
            return fileInfos.Select(x => x.FullName).ToArray();
        }

        /// <summary>
        /// Load extensions
        /// </summary>
        static void LoadExtensions()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] files = GetFiles(path, "Words.*.dll");
            foreach (string file in files)
            {
                Assembly assemblyForReflection = Assembly.LoadFile(file);
                // Input extensions
                List<Type> inputTypes =
                    assemblyForReflection.GetTypes()
                        .Where(x => x.IsClass && typeof(IWordsInput).IsAssignableFrom(x))
                        .ToList();
                if (inputTypes.Any())
                {
                    //Assembly.LoadFile(file);
                    foreach (Type inputType in inputTypes)
                    {
                        IWordsInput instance = (IWordsInput)Activator.CreateInstance(inputType);
                        InputExtensions.Add(instance);
                    }
                }
                // Output extensions
                List<Type> outputTypes =
                    assemblyForReflection.GetTypes()
                        .Where(x => x.IsClass && typeof(IWordsOutput).IsAssignableFrom(x))
                        .ToList();
                if (outputTypes.Any())
                {
                    //Assembly.LoadFile(file);
                    foreach (Type outputType in outputTypes)
                    {
                        IWordsOutput instance = (IWordsOutput)Activator.CreateInstance(outputType);
                        OutputExtensions.Add(instance);
                    }
                }
            }

            Console.WriteLine("Input extensions (prefix: description)");
            foreach (IWordsInput inputExtension in InputExtensions)
            {
                Console.WriteLine("{0}: {1}", inputExtension.Prefix, inputExtension.Description);
            }
            Console.WriteLine("Output extensions (prefix: description)");
            foreach (IWordsOutput outputExtension in OutputExtensions)
            {
                Console.WriteLine("{0}: {1}", outputExtension.Prefix, outputExtension.Description);
            }
        }

    }
}