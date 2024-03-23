using FluentAssertions;
using GoProVideoRenamer.File;
using GoProVideoRenamer.File.VideoFile;
using GoProVideoRenamer.File.VideoFile.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.File
{
    [TestClass]
    public class FileFilterTests
    {
        [TestMethod]
        public void FileFilter_ShouldReturnGoProAvcVideos()
        {
            var testFiles = new[] {
                IVideoFileMocks.GetMockedIVideoFile("GH010001.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GH010002.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GH010003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);
            
            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldReturnGoProHevcVideos()
        {
            var testFiles = new[] {
                IVideoFileMocks.GetMockedIVideoFile("GX010001.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GX010002.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GX010003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldIdentifySingleVideos()
        {
            var testFiles = new[] {
                IVideoFileMocks.GetMockedIVideoFile("GH010001.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GH010002.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GH010003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldIdentifyChapteredVideos()
        {
            var testFiles = new[] {
                IVideoFileMocks.GetMockedIVideoFile("GH010001.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GH020001.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GH030001.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldNotIdentifyLoopedVideos()
        {
            var testFiles = new[] {
                IVideoFileMocks.GetMockedIVideoFile("GHAA0001.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GHAA0002.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GHAA0003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().BeEmpty();
        }

        [TestMethod]
        public void FileFilter_ShouldNotReturnOtherFiles()
        {
            var goproFiles = new[] {
                IVideoFileMocks.GetMockedIVideoFile("GH010001.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GH020001.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("GH030001.mp4")
            };
            var notGoproFiles = new[] {
                IVideoFileMocks.GetMockedIVideoFile("test.txt"),
                IVideoFileMocks.GetMockedIVideoFile("other_video.mp4"),
                IVideoFileMocks.GetMockedIVideoFile("never gonna give you up.mp3"),
                IVideoFileMocks.GetMockedIVideoFile("system32")
            };
            var allFiles = goproFiles.Union(notGoproFiles);
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(allFiles);

            matchingVideos.Should().Contain(goproFiles);
            matchingVideos.Should().NotContain(notGoproFiles);
        }
    }
}
