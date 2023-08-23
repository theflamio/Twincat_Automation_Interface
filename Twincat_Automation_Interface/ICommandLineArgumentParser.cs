using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twincat_Automation_Interface
{
    public interface ICommandLineArgumentParser
    {
        void ParseArguments(string[] args);
        string GetArgumentValue(string argumentName);
        bool HasArgument(string argumentName);
    }
}
