using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Numbered;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;
using GoProVideoRenamer.File.VideoFiles.Renamed;

namespace GoProVideoRenamer.UnitTests.File.VideoFiles.Renamed
{
    [TestClass]
    public class RenamedVideoFileTests
    {
        [TestMethod]
        public void RenamedVideoFile_GivenINumberedVideoFile_HasCorrectProperties()
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

            var file = new RenamedVideoFile("new file 5.mp4", numberedMock.Object);

            Assert.AreEqual(10, file.NewIndex);
            Assert.AreEqual("GX020004.mp4", file.Name);
            Assert.AreEqual(".mp4", file.FileExtension);
            Assert.AreEqual(2, file.ChapterNumber);
            Assert.AreEqual(4, file.FileNumber);
            Assert.AreEqual("new file 5.mp4", file.NewName);
        }

        [TestMethod]
        public void RenamedVideoFile_GivenINumberedVideoFile_RenamesFileCorrectly()
        {
            var numberedMock = new Mock<INumberedVideoFile>();
            var fileInfoMock = new Mock<IFileInfo>();
            var directoryInfoMock = new Mock<IDirectoryInfo>();
            directoryInfoMock.Setup(m => m.FullName).Returns("Some directory full name");
            fileInfoMock.Setup(m => m.Directory).Returns(directoryInfoMock.Object);
            numberedMock.Setup(m => m.FileInfo).Returns(fileInfoMock.Object);
            var file = new RenamedVideoFile("new file 5.mp4", numberedMock.Object);

            file.CommitRenameToDisk();

            directoryInfoMock.Verify(m => m.FullName, Times.Once());
            fileInfoMock.Verify(m => m.MoveTo("Some directory full name\\new file 5.mp4"), Times.Once());
            directoryInfoMock.VerifyNoOtherCalls();
            fileInfoMock.VerifyNoOtherCalls();
        }
    }
}
