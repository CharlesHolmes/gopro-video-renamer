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
            var mockedAddFunc = new Mock<Func<ICoconaCommandsBuilder, Type, CommandTypeConventionBuilder>>();
            mockedAddFunc
                .Setup(m => m.Invoke(It.IsAny<ICoconaCommandsBuilder>(), It.IsAny<Type>()));
            CommandConfiguration.AddCommand = mockedAddFunc.Object;

            CommandConfiguration.RegisterAllCommands(mockedApp.Object);

            mockedAddFunc.Verify(m => m.Invoke(mockedApp.Object, It.Is<Type>(t => t == typeof(RenameCommand))));
        }
    }
}
