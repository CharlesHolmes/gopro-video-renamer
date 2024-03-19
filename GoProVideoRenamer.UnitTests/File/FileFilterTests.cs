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
                GetMockedIVideoFile("GH010001.mp4"),
                GetMockedIVideoFile("GH010002.mp4"),
                GetMockedIVideoFile("GH010003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);
            
            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldReturnGoProHevcVideos()
        {
            var testFiles = new[] {
                GetMockedIVideoFile("GX010001.mp4"),
                GetMockedIVideoFile("GX010002.mp4"),
                GetMockedIVideoFile("GX010003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldIdentifySingleVideos()
        {
            var testFiles = new[] {
                GetMockedIVideoFile("GH010001.mp4"),
                GetMockedIVideoFile("GH010002.mp4"),
                GetMockedIVideoFile("GH010003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldIdentifyChapteredVideos()
        {
            var testFiles = new[] {
                GetMockedIVideoFile("GH010001.mp4"),
                GetMockedIVideoFile("GH020001.mp4"),
                GetMockedIVideoFile("GH030001.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().Contain(testFiles);
        }

        [TestMethod]
        public void FileFilter_ShouldNotIdentifyLoopedVideos()
        {
            var testFiles = new[] {
                GetMockedIVideoFile("GHAA0001.mp4"),
                GetMockedIVideoFile("GHAA0002.mp4"),
                GetMockedIVideoFile("GHAA0003.mp4")
            };
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(testFiles);

            matchingVideos.Should().BeEmpty();
        }

        [TestMethod]
        public void FileFilter_ShouldNotReturnOtherFiles()
        {
            var goproFiles = new[] {
                GetMockedIVideoFile("GH010001.mp4"),
                GetMockedIVideoFile("GH020001.mp4"),
                GetMockedIVideoFile("GH030001.mp4")
            };
            var notGoproFiles = new[] {
                GetMockedIVideoFile("test.txt"),
                GetMockedIVideoFile("other_video.mp4"),
                GetMockedIVideoFile("never gonna give you up.mp3"),
                GetMockedIVideoFile("system32")
            };
            var allFiles = goproFiles.Union(notGoproFiles);
            var fileFilter = new FileFilter();

            var matchingVideos = fileFilter.GetMatchingVideos(allFiles);

            matchingVideos.Should().Contain(goproFiles);
            matchingVideos.Should().NotContain(notGoproFiles);
        }

        private IVideoFile GetMockedIVideoFile(string fileName)
        {
            var result = new Mock<IVideoFile>();
            result.Setup(m => m.Name).Returns(fileName);
            return result.Object;
        }
    }
}
