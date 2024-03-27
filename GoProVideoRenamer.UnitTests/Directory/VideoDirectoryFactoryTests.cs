﻿using Castle.Core.Logging;
using FluentAssertions;
using GoProVideoRenamer.Directory;
using GoProVideoRenamer.Directory.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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
    public class VideoDirectoryFactoryTests
    {
        private readonly ServiceCollection _serviceCollection = new ServiceCollection();
        private readonly Mock<ILogger<VideoDirectory>> _loggerMock = new Mock<ILogger<VideoDirectory>>();
        private readonly Mock<IFileSystem> _fileSystemMock = new Mock<IFileSystem>();
        private readonly Mock<IDirectoryInfoFactory> _directoryInfoFactoryMock = new Mock<IDirectoryInfoFactory>();
        private readonly Mock<IDirectoryInfo> _directoryInfoMock = new Mock<IDirectoryInfo>();
        private readonly Mock<IVideoFileFactory> _videoFileFactoryMock = new Mock<IVideoFileFactory>();
        private readonly string _directory = "asdf1234";

        [TestInitialize]
        public void Setup()
        {
            _directoryInfoMock.Setup(m => m.Exists).Returns(true);
            _directoryInfoFactoryMock.Setup(m => m.New(_directory)).Returns(_directoryInfoMock.Object);
            _fileSystemMock.Setup(m => m.DirectoryInfo).Returns(_directoryInfoFactoryMock.Object);
            _serviceCollection.Clear();
            _serviceCollection.AddSingleton<ILogger<VideoDirectory>>((_) => _loggerMock.Object);
            _serviceCollection.AddSingleton<IFileSystem>((_) => _fileSystemMock.Object);
            _serviceCollection.AddSingleton<IVideoFileFactory>((_) => _videoFileFactoryMock.Object);
        }

        [TestMethod]
        public void VideoDirectoryFactory_ShouldCreateDirectory()
        {
            var factory = new VideoDirectoryFactory(_serviceCollection.BuildServiceProvider());

            var videoDirectory = factory.Create(_directory);

            _fileSystemMock.Verify(m => m.DirectoryInfo);
            _directoryInfoFactoryMock.Verify(m => m.New(_directory));
            _directoryInfoMock.Verify(m => m.Exists);

            videoDirectory.Should().BeAssignableTo<IVideoDirectory>();
        }
    }
}
