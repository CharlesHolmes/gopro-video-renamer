using FluentAssertions;
using GoProVideoRenamer.File.VideoFiles;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.File.VideoFiles
{
    [TestClass]
    public class VideoFileFactoryTests
    {
        [TestMethod]
        public void Factory_ShouldInstantiateCorrectType()
        {
            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(m => m.Name).Returns("GX020004.mp4");
            fileInfoMock.Setup(m => m.Extension).Returns(".mp4");
            var factory = new VideoFileFactory();

            var result = factory.Create(fileInfoMock.Object);

            result.Should().BeAssignableTo<IVideoFile>();
        }
    }
}
