
namespace Twincat_Automation_Interface
{
    //Fixed
    class ArgumentValidator
    {
        private ICommandLineArgumentParser parser;

        public ArgumentValidator(string[] args, ICommandLineArgumentParser commandLineArgumentParser) 
        {
            // Error handling needed here check for null and argument is empty
            parser = commandLineArgumentParser;
            parser.ParseArguments(args);            
        }

        public string ValidateSolutionPath()
        {
            string solutionPath = parser.GetArgumentValue("visualStudioFilePath");

            if (File.Exists(solutionPath))
            {
                Console.WriteLine("Visual studio solution found!");
            }
            else
            {
                Console.WriteLine("Visual studio solution path dose not exist!");
                Environment.Exit(1);
            }
            return solutionPath;
        }

        public string ValidateAmsNetId()
        {
            string amsNetId = parser.GetArgumentValue("amsNetId");

            if (string.IsNullOrEmpty(amsNetId))
            {
                Console.WriteLine("No amsNetId provided, assuming local AmsNetId");
                amsNetId = "127.0.0.1.1.1";
                return amsNetId;
            }
            return amsNetId;
        }

        public bool ValidateCompileSolution()
        {
            if (parser.HasArgument("compileSolution"))
            {
                string compileSolution = parser.GetArgumentValue("compileSolution");

                return validateBoolean(compileSolution);
            }
            Console.WriteLine("No argument compileSolution was provided return false");
            return false;

        }

        public bool ValidateRunSolution()
        {
            if (parser.HasArgument("runSolution"))
            {
                string runSolution = parser.GetArgumentValue("runSolution");

                return validateBoolean(runSolution);
            }
            Console.WriteLine("No argument runSolution was provided return false");
            return false;
        }

        public void Help()
        { 
            if(parser.HasArgument("help"))
            {
                Console.WriteLine("How to use the twincat automation interface");
                Console.WriteLine("this program accept following arguments");
                Console.WriteLine("--visualStudioFilePath=path");
                Console.WriteLine("--amsNetId=ip .If no ip is provided default ip = 127.0.0.1.1.1");
                Console.WriteLine("--compileSolution=boolean .Default is true");
                Console.WriteLine("--runSolution=boolean .Default is false");
                Console.WriteLine("--help=help");
            }
        }

        private bool validateBoolean(string value)
        {
            // needs to be refactored !!! this is an ugly way to handle an error
            if (value.ToLower() == "true")
            {
                return true;
            }
            else if (value.ToLower() == "false")
            {
                return false;
            }
            else
            {
                Console.WriteLine($"{value} invalid argmunt must be true or false!");
                Environment.Exit(1);
                return false;
            }
        }
    }
}
