using EnvDTE80;

namespace Twincat_Automation_Interface
{
    class main
    {
        // Temp args for testing purpose
        public static string visualStudioFilePath = "--visualStudioFilePath=C:\\Dev\\TwinCatHelloWorld\\HelloWorld\\HelloWorld.sln";
        public static string amsNetId = "--amsNetId=";
        public static string compileSolution = "--compileSolution=false";
        public static string runSolution = "--runSolution=false";
        public static string help = "--help=false";

        public static string[] argsList = new string[] { visualStudioFilePath, amsNetId, compileSolution, runSolution, help };
        //

        static void Main(string[] args)
        {
            CommandLineArgumentParser parser = new CommandLineArgumentParser();

            ArgumentValidator argument = new ArgumentValidator(args, parser);

            // Compile a TwinCatProject

            Type t = System.Type.GetTypeFromProgID("TcXaeShell.DTE.15.0"); // TwinCAT XAE Shell is using version 15 see https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_installation/179467147.html&id=

            if(t != null)
            {
                EnvDTE80.DTE2 dte = (EnvDTE80.DTE2)System.Activator.CreateInstance(t); // Create instants of visual studio DTE "Development Tools Environment aka API for visual studio"
                dte.SuppressUI = false;
                dte.MainWindow.Visible = true;
                EnvDTE.Solution sol = dte.Solution;
                
                Task task = Task.Run(() => openVisualStudioFilePath(sol, visualStudioFilePath, 5000));
                task.Wait();

                // Clean and Build Solution "true = wait for finish build"

                sol.SolutionBuild.Clean(true);
                sol.SolutionBuild.Build(true);

                ErrorItems errors = dte.ToolWindows.ErrorList.ErrorItems;
                Console.WriteLine("Errors count: " + errors.Count);

                for (int i = 1; i <= errors.Count; i++)
                {
                    ErrorItem item = errors.Item(i);
                    Console.WriteLine("Description: " + item.Description);
                    Console.WriteLine("ErrorLevel: " + item.ErrorLevel);
                    Console.WriteLine("Filename: " + item.FileName);
                }
            }
            else
            {
                Console.WriteLine("System.Type.GetTypeFromProgID(\"TcXaeShell.DTE.15.0\") was null ");
                Environment.Exit(1);
            }
            



            // Here the twinCAT automation interface is starting
            // starts up the projet so we can cacht runtime errors


            //EnvDTE.Project pro = sol.Projects.Item(1);

            //ITcSysManager15 sysMan = (ITcSysManager15)pro.Object;

            //sysMan.ActivateConfiguration(); //
            //sysMan.StartRestartTwinCAT();   //

            //Run solution

            //sol.SolutionBuild.Run();
        }

        static void openVisualStudioFilePath(EnvDTE.Solution sol, string visualStudioFilePath, int delayMS)
        {
            sol.Open(visualStudioFilePath);
            Task.Delay(delayMS).Wait();
        }
    }
}