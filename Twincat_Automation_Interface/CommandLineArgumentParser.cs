using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twincat_Automation_Interface
{
    public class CommandLineArgumentParser : ICommandLineArgumentParser
    {
        private Dictionary<string, string> parsedArguments = new Dictionary<string, string>();
        private List<string> validArgumentNames = new List<string> { "visualStudioFilePath", "amsNetId", "compileSolution", "runSolution", "help"};

        public void ParseArguments(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg.StartsWith("--"))
                {
                    string[] parts = arg.Substring(2).Split('=');
                    if (parts.Length == 2)
                    {
                        string argumentName = parts[0];
                        string argumentValue = parts[1];
                        parsedArguments[argumentName] = argumentValue;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid argument format: {arg}");
                    }
                }
            }
        }

        public string GetArgumentValue(string argumentName)
        {
            if (parsedArguments.ContainsKey(argumentName))
            {
                return parsedArguments[argumentName];
            }
            else
            {
                Console.WriteLine($"Invalid argument value: {argumentName}");
                return "";
            }
        }

        public bool HasArgument(string argumentName)
        {
            return parsedArguments.ContainsKey(argumentName);
        }
    }
}
