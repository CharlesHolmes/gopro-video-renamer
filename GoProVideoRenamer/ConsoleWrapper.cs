using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string line)
        {
            Console.Out.WriteLine(line);
        }

        public void WriteErrorLine(string line)
        {
            Console.Error.WriteLine(line);
        }
    }
}
