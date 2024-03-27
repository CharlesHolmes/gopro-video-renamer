using FluentAssertions;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Numbered;
using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.File.VideoFiles.Numbered
{
    [TestClass]
    public class NumberedVideoFileFactoryTests
    {
        [TestMethod]
        public void Factory_ShouldCreateCorrectType()
        {
            var factory = new NumberedVideoFileFactory();

            var result = factory.Create(1, new Mock<IVideoFile>().Object);

            result.Should().BeAssignableTo<INumberedVideoFile>();
        }
    }
}
