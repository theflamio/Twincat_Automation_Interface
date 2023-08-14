using System;
using System.Threading.Tasks;

using EnvDTE80;
using TCatSysManagerLib;

namespace Twincat_Automation_Interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string visualStudioFilePath = "C:\\Dev\\TwinCatHelloWorld\\HelloWorld\\HelloWorld.sln";
            string amsNetId = "";
            bool showHelp = false;

            if (File.Exists(visualStudioFilePath) == false) 
            {
                Console.WriteLine("Visual studio solution dose not exist!");
                Environment.Exit(1);
            }

            if(string.IsNullOrEmpty(amsNetId))
            {
                Console.WriteLine("No amsNetId provided, assuming local AmsNetId");
                amsNetId = "127.0.0.1.1.1";
            }

            // Compile a TwinCatProject

            Type t = System.Type.GetTypeFromProgID("TcXaeShell.DTE.15.0"); // TwinCAT XAE Shell is using version 15 see https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_installation/179467147.html&id=
            EnvDTE80.DTE2 dte = (EnvDTE80.DTE2)System.Activator.CreateInstance(t); // Create instants of visual studio DTE "Development Tools Environment aka API for visual studio"
            dte.SuppressUI = false;
            dte.MainWindow.Visible = true;

            EnvDTE.Solution sol = dte.Solution;

            Task task = Task.Run(() => openVisualStudioFilePath(sol,visualStudioFilePath));
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

            // Here the twinCAT automation interface is starting
            // starts up the projet so we can cacht runtime errors


            //EnvDTE.Project pro = sol.Projects.Item(1);

            //ITcSysManager15 sysMan = (ITcSysManager15)pro.Object;

            //sysMan.ActivateConfiguration(); //
            //sysMan.StartRestartTwinCAT();   //

            //Run solution

            //sol.SolutionBuild.Run();
        }

        static void openVisualStudioFilePath(EnvDTE.Solution sol, string visualStudioFilePath)
        {
            sol.Open(visualStudioFilePath);
            Task.Delay(5000).Wait();
        }
    }
}