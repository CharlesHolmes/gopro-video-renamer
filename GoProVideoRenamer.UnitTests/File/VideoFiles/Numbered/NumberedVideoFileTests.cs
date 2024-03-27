using GoProVideoRenamer.File.VideoFiles.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Numbered;
using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.File.VideoFiles.Numbered
{
    [TestClass]
    public class NumberedVideoFileTests
    {
        [TestMethod]
        public void NumberedVideoFile_GivenIVideoFile_HasCorrectProperties()
        {
            var videoFileMock = new Mock<IVideoFile>();
            videoFileMock.Setup(m => m.Name).Returns("GX020004.mp4");
            videoFileMock.Setup(m => m.FileExtension).Returns(".mp4");
            videoFileMock.Setup(m => m.ChapterNumber).Returns(2);
            videoFileMock.Setup(m => m.FileNumber).Returns(4);
            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(m => m.Name).Returns("GX020004.mp4");
            fileInfoMock.Setup(m => m.Extension).Returns(".mp4");
            videoFileMock.Setup(m => m.FileInfo).Returns(fileInfoMock.Object);

            var file = new NumberedVideoFile(9, videoFileMock.Object);

            Assert.AreEqual(9, file.NewIndex);
            Assert.AreEqual("GX020004.mp4", file.Name);
            Assert.AreEqual(".mp4", file.FileExtension);
            Assert.AreEqual(2, file.ChapterNumber);
            Assert.AreEqual(4, file.FileNumber);
        }

        [TestMethod]
        public void NumberedVideoFile_GivenINumberedVideoFile_HasCorrectProperties()
        {
            var numberedMock = new Mock<INumberedVideoFile>();
            numberedMock.Setup(m => m.Name).Returns("GX020004.mp4");
            numberedMock.Setup(m => m.FileExtension).Returns(".mp4");
            numberedMock.Setup(m => m.ChapterNumber).Returns(2);
            numberedMock.Setup(m => m.FileNumber).Returns(4);
            numberedMock.Setup(m => m.NewIndex).Returns(10);
            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(m => m.Name).Returns("GX020004.mp4");
            fileInfoMock.Setup(m => m.Extension).Returns(".mp4");
            numberedMock.Setup(m => m.FileInfo).Returns(fileInfoMock.Object);

            var file = new NumberedVideoFile(numberedMock.Object);

            Assert.AreEqual(10, file.NewIndex);
            Assert.AreEqual("GX020004.mp4", file.Name);
            Assert.AreEqual(".mp4", file.FileExtension);
            Assert.AreEqual(2, file.ChapterNumber);
            Assert.AreEqual(4, file.FileNumber);
        }
    }
}
