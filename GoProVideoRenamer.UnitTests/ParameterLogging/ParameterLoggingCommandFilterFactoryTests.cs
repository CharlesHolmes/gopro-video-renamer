using Castle.Core.Logging;
using Cocona.Filters;
using FluentAssertions;
using GoProVideoRenamer.ParameterLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.ParameterLogging
{
    [TestClass]
    public class ParameterLoggingCommandFilterFactoryTests
    {
        [TestMethod]
        public void Factory_ShouldCreateFilterOfCorrectType()
        {
            var factory = new ParameterLoggingCommandFilterFactory();
            var logger = new Mock<ILogger<ParameterLoggingCommandFilter>>();
            var services = new ServiceCollection()
                .AddSingleton<ILogger<ParameterLoggingCommandFilter>>(logger.Object)
                .BuildServiceProvider();

            var instance = factory.CreateInstance(services);

            instance.Should().BeAssignableTo<IFilterMetadata>();
            instance.Should().BeAssignableTo<ICommandFilter>();
            instance.Should().BeAssignableTo<ParameterLoggingCommandFilter>();
        }
    }
}
