using FluentAssertions;
using GoProVideoRenamer.File;
using GoProVideoRenamer.File.VideoFile.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFile.Renamed;
using GoProVideoRenamer.File.VideoFile.Renamed.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.File
{
    [TestClass]
    public class FileRenameTests
    {
        private readonly Mock<ILogger<FileRename>> _loggerMock = new Mock<ILogger<FileRename>>();
        private readonly Mock<IRenamedVideoFileFactory> _factory = new Mock<IRenamedVideoFileFactory>();

        [TestInitialize]
        public void Setup()
        {
            _factory.Reset();
        }

        [TestMethod]
        public void FileRename_ShouldWorkWhenPrefixSupplied()
        {
            CheckPrefixSuffixNaming(
                "some result - ", 
                null, 
                [
                    "some result - 1.mp4",
                    "some result - 2.mp4",
                    "some result - 3.mp4",
                    "some result - 4.mp4",
                    "some result - 5.mp4",
                ]);
        }

        [TestMethod]
        public void FileRename_ShouldWorkWhenSuffixSupplied()
        {
            CheckPrefixSuffixNaming(
                null,
                " - some suffix to follow",
                [
                    "1 - some suffix to follow.mp4",
                    "2 - some suffix to follow.mp4",
                    "3 - some suffix to follow.mp4",
                    "4 - some suffix to follow.mp4",
                    "5 - some suffix to follow.mp4",
                ]);
        }

        [TestMethod]
        public void FileRename_ShouldWorkWhenPrefixAndSuffixSupplied()
        {
            CheckPrefixSuffixNaming(
                "a prefix - ",
                " - and a suffix",
                [
                    "a prefix - 1 - and a suffix.mp4",
                    "a prefix - 2 - and a suffix.mp4",
                    "a prefix - 3 - and a suffix.mp4",
                    "a prefix - 4 - and a suffix.mp4",
                    "a prefix - 5 - and a suffix.mp4",
                ]);
        }

        [TestMethod]
        public void FileRename_ShouldWorkWhenNeitherPrefixNorSuffixSupplied()
        {
            CheckPrefixSuffixNaming(
                null,
                null,
                [
                    "1.mp4",
                    "2.mp4",
                    "3.mp4",
                    "4.mp4",
                    "5.mp4",
                ]);
        }

        [TestMethod]
        public void FileRename_ShouldWorkWhenDigitCountSpecified()
        {
            var input = GetMockedInput(5);
            foreach (INumberedVideoFile file in input)
            {
                _factory.Setup(m => m.Create(It.IsAny<string>(), file))
                    .Returns<string, INumberedVideoFile>((s, f) => GetRenamed(f, s));
            }

            var rename = new FileRename(_loggerMock.Object, _factory.Object);

            var result = rename.GetRenamedFiles(input, null, null, 3);

            result.Select(r => r.NewName).Should().Equal([
                "001.mp4",
                "002.mp4",
                "003.mp4",
                "004.mp4",
                "005.mp4",
            ]);
            foreach (INumberedVideoFile file in input)
            {
                _factory.Verify(m => m.Create(It.IsAny<string>(), file), Times.Once());
            }
        }

        [TestMethod]
        public void FileRename_ShouldPadWhenDigitCountNotSpecified()
        {
            var input = GetMockedInput(10);
            foreach (INumberedVideoFile file in input)
            {
                _factory.Setup(m => m.Create(It.IsAny<string>(), file))
                    .Returns<string, INumberedVideoFile>((s, f) => GetRenamed(f, s));
            }

            var rename = new FileRename(_loggerMock.Object, _factory.Object);

            var result = rename.GetRenamedFiles(input, null, null, null);

            result.Select(r => r.NewName).Should().Equal([
                "01.mp4",
                "02.mp4",
                "03.mp4",
                "04.mp4",
                "05.mp4",
                "06.mp4",
                "07.mp4",
                "08.mp4",
                "09.mp4",
                "10.mp4"
            ]);
            foreach (INumberedVideoFile file in input)
            {
                _factory.Verify(m => m.Create(It.IsAny<string>(), file), Times.Once());
            }
        }

        [TestMethod]
        public void FileRename_ShouldThrowIfSpecifiedDigitsTooLow()
        {
            var input = GetMockedInput(10);
            foreach (INumberedVideoFile file in input)
            {
                _factory.Setup(m => m.Create(It.IsAny<string>(), file))
                    .Returns<string, INumberedVideoFile>((s, f) => GetRenamed(f, s));
            }

            var rename = new FileRename(_loggerMock.Object, _factory.Object);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => rename.GetRenamedFiles(input, null, null, 1));
        }

        private void CheckPrefixSuffixNaming(string? prefix, string? suffix, params string[] expectedFilenames)
        {
            var input = GetMockedInput(5);
            foreach (INumberedVideoFile file in input)
            {
                _factory.Setup(m => m.Create(It.IsAny<string>(), file))
                    .Returns<string, INumberedVideoFile>((s, f) => GetRenamed(f, s));
            }

            var rename = new FileRename(_loggerMock.Object, _factory.Object);

            var result = rename.GetRenamedFiles(input, prefix, suffix, null);

            result.Select(r => r.NewName).Should().Equal(expectedFilenames);
            foreach (INumberedVideoFile file in input)
            {
                _factory.Verify(m => m.Create(It.IsAny<string>(), file), Times.Once());
            }
        }

        private IRenamedVideoFile GetRenamed(INumberedVideoFile input, string newName)
        {
            var result = new Mock<IRenamedVideoFile>();
            result.Setup(m => m.NewName).Returns(newName);
            return result.Object;
        }

        private IList<INumberedVideoFile> GetMockedInput(int count)
        {
            var result = new List<INumberedVideoFile>();
            for (int i = 0; i < count; i++)
            {
                var file = new Mock<INumberedVideoFile>();
                file.Setup(m => m.FileExtension).Returns(".mp4");
                file.Setup(m => m.Name).Returns($"some input file.mp4");
                file.Setup(m => m.NewIndex).Returns(i + 1);
                result.Add(file.Object);
            }

            return result;
        }
    }
}
