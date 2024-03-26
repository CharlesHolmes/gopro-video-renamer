using FluentAssertions;
using GoProVideoRenamer.File;
using GoProVideoRenamer.File.VideoFile.Interfaces;
using GoProVideoRenamer.File.VideoFile.Numbered;
using GoProVideoRenamer.File.VideoFile.Numbered.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.File
{
    [TestClass]
    public class FileSortTests
    {
        [TestMethod]
        public void FileSort_ShouldOrderGoproFiles()
        {
            var file1 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX010001.mp4");
            var numbered1 = new NumberedVideoFile(1, file1);
            var file2 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX010002.mp4");
            var numbered2 = new NumberedVideoFile(2, file2);
            var file3 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX010003.mp4");
            var numbered3 = new NumberedVideoFile(3, file3);
            var file4 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX010004.mp4");
            var numbered4 = new NumberedVideoFile(4, file4);

            var files = new[]
            {
                file1,
                file4,
                file2,
                file3
            };

            var numberedFactory = new Mock<INumberedVideoFileFactory>();
            numberedFactory.Setup(m => m.Create(1, file1)).Returns(numbered1);
            numberedFactory.Setup(m => m.Create(2, file2)).Returns(numbered2);
            numberedFactory.Setup(m => m.Create(3, file3)).Returns(numbered3);
            numberedFactory.Setup(m => m.Create(4, file4)).Returns(numbered4);
            var fileSort = new FileSort(numberedFactory.Object);

            var result = fileSort.GetOrderedFiles(files, null);

            result.Should().Equal([numbered1, numbered2, numbered3, numbered4]);
        }

        [TestMethod]
        public void FileSort_ShouldOrderChapteredFiles()
        {
            var file1 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX010001.mp4");
            var numbered1 = new NumberedVideoFile(1, file1);
            var file2 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX020001.mp4");
            var numbered2 = new NumberedVideoFile(2, file2);
            var file3 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX030001.mp4");
            var numbered3 = new NumberedVideoFile(3, file3);
            var file4 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX040001.mp4");
            var numbered4 = new NumberedVideoFile(4, file4);

            var files = new[]
            {
                file1,
                file4,
                file2,
                file3
            };

            var numberedFactory = new Mock<INumberedVideoFileFactory>();
            numberedFactory.Setup(m => m.Create(1, file1)).Returns(numbered1);
            numberedFactory.Setup(m => m.Create(2, file2)).Returns(numbered2);
            numberedFactory.Setup(m => m.Create(3, file3)).Returns(numbered3);
            numberedFactory.Setup(m => m.Create(4, file4)).Returns(numbered4);
            var fileSort = new FileSort(numberedFactory.Object);

            var result = fileSort.GetOrderedFiles(files, null);

            result.Should().Equal([numbered1, numbered2, numbered3, numbered4]);
        }

        [TestMethod]
        public void FileSort_ShouldOrderIndividualAndChapteredFiles()
        {
            var file1 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX010001.mp4");
            var numbered1 = new NumberedVideoFile(1, file1);
            var file2 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX010002.mp4");
            var numbered2 = new NumberedVideoFile(2, file2);
            var file3 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX020002.mp4");
            var numbered3 = new NumberedVideoFile(3, file3);
            var file4 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX030002.mp4");
            var numbered4 = new NumberedVideoFile(4, file4);
            var file5 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX040002.mp4");
            var numbered5 = new NumberedVideoFile(5, file5);
            var file6 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX010003.mp4");
            var numbered6 = new NumberedVideoFile(6, file6);

            var files = new[]
            {
                file1,
                file4,
                file5,
                file2,
                file6,
                file3
            };

            var numberedFactory = new Mock<INumberedVideoFileFactory>();
            numberedFactory.Setup(m => m.Create(1, file1)).Returns(numbered1);
            numberedFactory.Setup(m => m.Create(2, file2)).Returns(numbered2);
            numberedFactory.Setup(m => m.Create(3, file3)).Returns(numbered3);
            numberedFactory.Setup(m => m.Create(4, file4)).Returns(numbered4);
            numberedFactory.Setup(m => m.Create(5, file5)).Returns(numbered5);
            numberedFactory.Setup(m => m.Create(6, file6)).Returns(numbered6);
            var fileSort = new FileSort(numberedFactory.Object);

            var result = fileSort.GetOrderedFiles(files, null);

            result.Should().Equal([numbered1, numbered2, numbered3, numbered4, numbered5, numbered6]);
        }

        [TestMethod]
        public void FileSort_ShouldThrowIfFilesAreDuplicate()
        {
            var file1 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX010001.mp4");
            var numbered1 = new NumberedVideoFile(1, file1);
            var file2 = IVideoFileMocking.GetIVideoFileWithMockedIFileInfo("GX010001.mp4");
            var numbered2 = new NumberedVideoFile(2, file2);

            var files = new[]
            {
                file1,
                file2
            };

            var numberedFactory = new Mock<INumberedVideoFileFactory>();
            numberedFactory.Setup(m => m.Create(1, file1)).Returns(numbered1);
            numberedFactory.Setup(m => m.Create(2, file2)).Returns(numbered2);
            var fileSort = new FileSort(numberedFactory.Object);

            Assert.ThrowsException<ArgumentException>(() => fileSort.GetOrderedFiles(files, null));
        }
    }
}
