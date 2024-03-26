using Castle.Core.Logging;
using Cocona.Command;
using Cocona.CommandLine;
using Cocona.Filters;
using GoProVideoRenamer.ParameterLogging;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.ParameterLogging
{
    [TestClass]
    public class ParameterLoggingCommandFilterTests
    {
        private readonly Mock<ILogger<ParameterLoggingCommandFilter>> _logger = new Mock<ILogger<ParameterLoggingCommandFilter>>();

        [TestInitialize]
        public void Setup()
        {
            _logger.Reset();
        }

        [TestMethod]
        public async Task Filter_ShouldDoThing()
        {
            var filter = new ParameterLoggingCommandFilter(_logger.Object);
            var ctx = new CoconaCommandExecutingContext(
                new CommandDescriptor(
                    new Mock<MethodInfo>().Object,
                    null,
                    "descriptor",
                    ImmutableList.Create<string>(),
                    "descriptor_description",
                    ImmutableList.Create<object>(),
                    ImmutableList.Create<ICommandParameterDescriptor>(),
                    ImmutableList.Create<CommandOptionDescriptor>(),
                    ImmutableList.Create<CommandArgumentDescriptor>(),
                    ImmutableList.Create<CommandOverloadDescriptor>(),
                    ImmutableList.Create<CommandOptionLikeCommandDescriptor>(),
                    CommandFlags.None,
                    null),
                new ParsedCommandLine(
                    ImmutableList.CreateRange<CommandOption>(
                        [
                            new CommandOption(
                                new CommandOptionDescriptor(
                                    typeof(string),
                                    "someopt",
                                    "someopt".ToArray(),
                                    "the description of the opt",
                                    CoconaDefaultValue.None,
                                    null,
                                    CommandOptionFlags.None,
                                    ImmutableList.Create<Attribute>()),
                                "25",
                                1),
                            new CommandOption(
                                new CommandOptionDescriptor(
                                    typeof(string),
                                    "food",
                                    "food".ToArray(),
                                    "what food to eat",
                                    CoconaDefaultValue.None,
                                    null,
                                    CommandOptionFlags.None,
                                    ImmutableList.Create<Attribute>()),
                                "steak",
                                2)
                        ]),
                    ImmutableList.Create<CommandArgument>(),
                    ImmutableList.Create<string>()),
                null);
            var next = new Mock<CommandExecutionDelegate>();
            next.Setup(m => m.Invoke(ctx)).ReturnsAsync(1);
            await filter.OnCommandExecutionAsync(ctx, next.Object);
            next.Verify(m => m.Invoke(ctx));
            next.VerifyNoOtherCalls();

            // Is there another way to do this?
            // https://stackoverflow.com/questions/66307477/how-to-verify-iloggert-log-extension-method-has-been-called-using-moq
            _logger.Verify(
                m => m.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Received option someopt with value 25." && @type.Name == "FormattedLogValues"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
            _logger.Verify(
                m => m.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Received option food with value steak." && @type.Name == "FormattedLogValues"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
            _logger.VerifyNoOtherCalls();
        }
    }
}
