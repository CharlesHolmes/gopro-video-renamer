using FluentAssertions;
using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Renamed;
using GoProVideoRenamer.File.VideoFiles.Renamed.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.File.VideoFiles.Renamed
{
    [TestClass]
    public class RenamedVideoFileFactoryTests
    {
        [TestMethod]
        public void Factory_ShouldProduceCorrectType()
        {
            var numberedMock = new Mock<INumberedVideoFile>();
            var factory = new RenamedVideoFileFactory();

            var result = factory.Create("some new name", numberedMock.Object);

            result.Should().BeAssignableTo<IRenamedVideoFile>();
        }
    }
}
