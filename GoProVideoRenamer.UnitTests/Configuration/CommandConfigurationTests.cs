using Cocona.Builder;
using Cocona.Filters;
using GoProVideoRenamer.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.Configuration
{
    [TestClass]
    public class CommandConfigurationTests
    {
        [TestMethod]
        public void CommandConfiguration_ShouldAddRenameCommand()
        {
            var mockedApp = new Mock<ICoconaCommandsBuilder>();
            mockedApp.DefaultValue = DefaultValue.Mock;

            CommandConfiguration.RegisterAllCommands(mockedApp.Object);

            mockedApp.Verify(m => m.Add(It.Is<TypeCommandDataSource>(t => ((TypeCommandData)t.Build()).Type == typeof(RenameCommand))));
        }
    }
}
