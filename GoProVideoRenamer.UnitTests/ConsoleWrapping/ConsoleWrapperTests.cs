using GoProVideoRenamer.ConsoleWrapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.ConsoleWrapping
{
    [TestClass]
    public class ConsoleWrapperTests
    {
        [TestMethod]
        [DoNotParallelize]
        public void ConsoleWrapper_ShouldWriteToConsoleOut()
        {
            string expected = "hello";
            var writer = new StringWriter();
            Console.SetOut(writer);
            var consoleWrapper = new ConsoleWrapper();

            consoleWrapper.WriteLine(expected);

            Assert.AreEqual(expected + Environment.NewLine, writer.ToString());
            Console.SetOut(Console.Out);
        }

        [TestMethod]
        [DoNotParallelize]
        public void ConsoleWrapper_ShouldWriteToConsoleError()
        {
            string expected = "hello";
            var writer = new StringWriter();
            Console.SetError(writer);
            var consoleWrapper = new ConsoleWrapper();

            consoleWrapper.WriteErrorLine(expected);

            Assert.AreEqual(expected + Environment.NewLine, writer.ToString());
            Console.SetError(Console.Error);
        }
    }
}
