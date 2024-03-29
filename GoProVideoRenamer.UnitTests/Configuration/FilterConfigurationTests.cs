using Cocona.Builder;
using Cocona.Filters;
using GoProVideoRenamer.Configuration;
using GoProVideoRenamer.ParameterLogging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.Configuration
{
    [TestClass]
    public class FilterConfigurationTests
    {
        [TestMethod]
        public void FilterConfiguration_ShouldRegisterParameterLoggingFilter()
        {
            var mockedApp = new Mock<ICoconaCommandsBuilder>();
            var mockedRegistrationFunc = new Mock<Func<ICoconaCommandsBuilder, IFilterFactory, ICoconaCommandsBuilder>>();
            mockedRegistrationFunc
                .Setup(m => m.Invoke(It.IsAny<ICoconaCommandsBuilder>(), It.IsAny<IFilterFactory>()))
                .Returns(mockedApp.Object);
            FilterConfiguration.RegisterFilter = mockedRegistrationFunc.Object;

            FilterConfiguration.RegisterAllFilters(mockedApp.Object);

            mockedRegistrationFunc.Verify(m => m.Invoke(mockedApp.Object, It.IsAny<ParameterLoggingCommandFilterFactory>()));
        }
    }
}
