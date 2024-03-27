using FluentAssertions;
using GoProVideoRenamer.File;
using GoProVideoRenamer.File.VideoFiles;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
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
                IVideoFileMocking.GetMockedIVideoFile("GH010001.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GH010002.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GH010003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);
            
            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldReturnGoProHevcVideos()
        {
            var testFiles = new[] {
                IVideoFileMocking.GetMockedIVideoFile("GX010001.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GX010002.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GX010003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldIdentifySingleVideos()
        {
            var testFiles = new[] {
                IVideoFileMocking.GetMockedIVideoFile("GH010001.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GH010002.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GH010003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldIdentifyChapteredVideos()
        {
            var testFiles = new[] {
                IVideoFileMocking.GetMockedIVideoFile("GH010001.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GH020001.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GH030001.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldNotIdentifyLoopedVideos()
        {
            var testFiles = new[] {
                IVideoFileMocking.GetMockedIVideoFile("GHAA0001.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GHAA0002.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GHAA0003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().BeEmpty();
        }

        [TestMethod]
        public void FileFilter_ShouldNotReturnOtherFiles()
        {
            var goproFiles = new[] {
                IVideoFileMocking.GetMockedIVideoFile("GH010001.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GH020001.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("GH030001.mp4")
            };
            var notGoproFiles = new[] {
                IVideoFileMocking.GetMockedIVideoFile("test.txt"),
                IVideoFileMocking.GetMockedIVideoFile("other_video.mp4"),
                IVideoFileMocking.GetMockedIVideoFile("never gonna give you up.mp3"),
                IVideoFileMocking.GetMockedIVideoFile("system32")
            };
            var allFiles = goproFiles.Union(notGoproFiles);
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(allFiles);

            matchingVideos.Should().Contain(goproFiles);
            matchingVideos.Should().NotContain(notGoproFiles);
        }
    }
}
