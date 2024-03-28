using FluentAssertions;
using GoProVideoRenamer.Configuration;
using GoProVideoRenamer.ConsoleWrapping;
using GoProVideoRenamer.Directory;
using GoProVideoRenamer.Directory.Interfaces;
using GoProVideoRenamer.File;
using GoProVideoRenamer.File.Interfaces;
using GoProVideoRenamer.File.VideoFiles;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Numbered;
using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Renamed;
using GoProVideoRenamer.File.VideoFiles.Renamed.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.Configuration
{
    [TestClass]
    public class ServiceConfigurationTests
    {
        [TestMethod]
        public void ServiceConfiguration_ShouldRegisterConsoleWrapper()
        {
            var collection = new ServiceCollection();
            collection.AddLogging();

            ServiceConfiguration.Configure(collection);

            collection
                .BuildServiceProvider()
                .GetRequiredService<IConsoleWrapper>()
                .Should()
                .BeOfType<ConsoleWrapper>();
        }

        [TestMethod]
        public void ServiceConfiguration_ShouldRegisterFileSystem()
        {
            var collection = new ServiceCollection();
            collection.AddLogging();

            ServiceConfiguration.Configure(collection);

            collection
                .BuildServiceProvider()
                .GetRequiredService<IFileSystem>()
                .Should()
                .BeOfType<FileSystem>();
        }

        [TestMethod]
        public void ServiceConfiguration_ShouldRegisterVideoDirectoryFactory()
        {
            var collection = new ServiceCollection();
            collection.AddLogging();

            ServiceConfiguration.Configure(collection);

            collection
                .BuildServiceProvider()
                .GetRequiredService<IVideoDirectoryFactory>()
                .Should()
                .BeOfType<VideoDirectoryFactory>();
        }

        [TestMethod]
        public void ServiceConfiguration_ShouldRegisterVideoFileFactory()
        {
            var collection = new ServiceCollection();
            collection.AddLogging();

            ServiceConfiguration.Configure(collection);

            collection
                .BuildServiceProvider()
                .GetRequiredService<IVideoFileFactory>()
                .Should()
                .BeOfType<VideoFileFactory>();
        }

        [TestMethod]
        public void ServiceConfiguration_ShouldRegisterNumberedVideoFileFactory()
        {
            var collection = new ServiceCollection();
            collection.AddLogging();

            ServiceConfiguration.Configure(collection);

            collection
                .BuildServiceProvider()
                .GetRequiredService<INumberedVideoFileFactory>()
                .Should()
                .BeOfType<NumberedVideoFileFactory>();
        }

        [TestMethod]
        public void ServiceConfiguration_ShouldRegisterRenamedVideoFileFactory()
        {
            var collection = new ServiceCollection();
            collection.AddLogging();

            ServiceConfiguration.Configure(collection);

            collection
                .BuildServiceProvider()
                .GetRequiredService<IRenamedVideoFileFactory>()
                .Should()
                .BeOfType<RenamedVideoFileFactory>();
        }

        [TestMethod]
        public void ServiceConfiguration_ShouldRegisterFileFilter()
        {
            var collection = new ServiceCollection();
            collection.AddLogging();

            ServiceConfiguration.Configure(collection);

            collection
                .BuildServiceProvider()
                .GetRequiredService<IFileFilter>()
                .Should()
                .BeOfType<FileFilter>();
        }

        [TestMethod]
        public void ServiceConfiguration_ShouldRegisterFileRename()
        {
            var collection = new ServiceCollection();
            collection.AddLogging();

            ServiceConfiguration.Configure(collection);

            collection
                .BuildServiceProvider()
                .GetRequiredService<IFileRename>()
                .Should()
                .BeOfType<FileRename>();
        }

        [TestMethod]
        public void ServiceConfiguration_ShouldRegisterFileSort()
        {
            var collection = new ServiceCollection();
            collection.AddLogging();

            ServiceConfiguration.Configure(collection);

            collection
                .BuildServiceProvider()
                .GetRequiredService<IFileSort>()
                .Should()
                .BeOfType<FileSort>();
        }
    }
}
