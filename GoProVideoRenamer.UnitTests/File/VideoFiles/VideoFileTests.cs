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
    public class VideoFileTests
    {
        [TestMethod]
        public void VideoFile_GivenIFileInfo_HasCorrectProperties()
        {
            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(m => m.Name).Returns("GX020004.mp4");
            fileInfoMock.Setup(m => m.Extension).Returns(".mp4");

            var file = new VideoFile(fileInfoMock.Object);

            Assert.AreEqual(2, file.ChapterNumber);
            Assert.AreEqual(4, file.FileNumber);
            Assert.AreEqual(fileInfoMock.Object, file.FileInfo);
            Assert.AreEqual(".mp4", file.FileExtension);
            Assert.AreEqual("GX020004.mp4", file.Name);
        }

        [TestMethod]
        public void VideoFile_GivenIVideoFile_HasCorrectProperties()
        {
            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(m => m.Name).Returns("GX020004.mp4");
            fileInfoMock.Setup(m => m.Extension).Returns(".mp4");
            var videoFileMock = new Mock<IVideoFile>();
            videoFileMock.Setup(m => m.FileInfo).Returns(fileInfoMock.Object);

            var file = new VideoFile(videoFileMock.Object);

            Assert.AreEqual(2, file.ChapterNumber);
            Assert.AreEqual(4, file.FileNumber);
            Assert.AreEqual(fileInfoMock.Object, file.FileInfo);
            Assert.AreEqual(".mp4", file.FileExtension);
            Assert.AreEqual("GX020004.mp4", file.Name);
        }
    }
}
