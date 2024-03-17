using FluentAssertions;
using GoProVideoRenamer.Directory;
using GoProVideoRenamer.File.VideoFile;
using GoProVideoRenamer.File.VideoFile.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.Directory
{
    [TestClass]
    public class VideoDirectoryTests
    {
        private Mock<ILogger<VideoDirectory>> _loggerMock = new Mock<ILogger<VideoDirectory>>();
        private Mock<IFileSystem> _fileSystemMock = new Mock<IFileSystem>();
        private Mock<IDirectoryInfoFactory> _directoryInfoFactoryMock = new Mock<IDirectoryInfoFactory>();
        private Mock<IVideoFileFactory> _videoFileFactoryMock = new Mock<IVideoFileFactory>();
        private readonly string _directoryPath = "asdf1234";

        [TestInitialize]
        public void Setup()
        {
            _loggerMock.Reset();
            _fileSystemMock.Reset();
            _directoryInfoFactoryMock.Reset();
            _fileSystemMock.Setup(m => m.DirectoryInfo).Returns(_directoryInfoFactoryMock.Object);
            _videoFileFactoryMock.Reset();
        }

        [TestMethod]
        public void VideoDirectory_ShouldThrow_WhenDirectoryDoesNotExist()
        {
            var badDirectory = new Mock<IDirectoryInfo>();
            badDirectory.Setup(m => m.Exists).Returns(false);
            _directoryInfoFactoryMock.Setup(m => m.New(_directoryPath)).Returns(badDirectory.Object);

            Assert.ThrowsException<DirectoryNotFoundException>(
                () => new VideoDirectory(
                    _loggerMock.Object,
                    _fileSystemMock.Object,
                    _videoFileFactoryMock.Object,
                    _directoryPath));
        }

        [TestMethod]
        public void VideoDirectory_ShouldCreateVideoFileObjects()
        {
            int fileCount = 5;
            IFileInfo[] directoryFiles = new IFileInfo[fileCount];
            IVideoFile[] expected = new IVideoFile[fileCount];
            for (int i = 0; i < fileCount; i++)
            {
                directoryFiles[i] = new Mock<IFileInfo>().Object;
                expected[i] = new Mock<IVideoFile>().Object;
                _videoFileFactoryMock.Setup(m => m.Create(directoryFiles[i])).Returns(expected[i]);
            }

            var goodDirectory = new Mock<IDirectoryInfo>();
            goodDirectory.Setup(m => m.Exists).Returns(true);
            goodDirectory.Setup(m => m.GetFiles()).Returns(directoryFiles);
            _directoryInfoFactoryMock.Setup(m => m.New(_directoryPath)).Returns(goodDirectory.Object);
            var videoDirectory = new VideoDirectory(
                    _loggerMock.Object,
                    _fileSystemMock.Object,
                    _videoFileFactoryMock.Object,
                    _directoryPath);

            var result = videoDirectory.GetFilesInDirectory();

            result.Should().Contain(expected);
            for (int i = 0; i < fileCount; i++)
            {
                _videoFileFactoryMock.Verify(m => m.Create(directoryFiles[i]));
            }
        }
    }
}
