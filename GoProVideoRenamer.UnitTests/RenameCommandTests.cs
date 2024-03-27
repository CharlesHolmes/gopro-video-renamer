using GoProVideoRenamer.ConsoleWrapping;
using GoProVideoRenamer.Directory.Interfaces;
using GoProVideoRenamer.File.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Renamed.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests
{
    [TestClass]
    public class RenameCommandTests
    {
        private readonly Mock<ILogger<RenameCommand>> _loggerMock = new Mock<ILogger<RenameCommand>>();
        private readonly Mock<IVideoDirectoryFactory> _directoryFactoryMock = new Mock<IVideoDirectoryFactory>();
        private readonly Mock<IVideoDirectory> _directoryMock = new Mock<IVideoDirectory>();
        private readonly Mock<IVideoFile> _testFile1 = new Mock<IVideoFile>();
        private readonly Mock<IVideoFile> _testFile2 = new Mock<IVideoFile>();
        private readonly Mock<IVideoFile> _testFile3 = new Mock<IVideoFile>();
        private readonly Mock<IFileFilter> _fileFilterMock = new Mock<IFileFilter>();
        private readonly Mock<IFileSort> _fileSortMock = new Mock<IFileSort>();
        private readonly Mock<INumberedVideoFile> _testNumbered1 = new Mock<INumberedVideoFile>();
        private readonly Mock<INumberedVideoFile> _testNumbered2 = new Mock<INumberedVideoFile>();
        private readonly Mock<IFileRename> _fileRenameMock = new Mock<IFileRename>();
        private readonly Mock<IRenamedVideoFile> _testRenamed1 = new Mock<IRenamedVideoFile>();
        private readonly Mock<IRenamedVideoFile> _testRenamed2 = new Mock<IRenamedVideoFile>();
        private readonly Mock<IConsoleWrapper> _consoleWrapperMock = new Mock<IConsoleWrapper>();
        private readonly string _fileLocation = "asdf1234";
        private readonly string _prefix = "prefix1234";
        private readonly string _suffix = "1234suffix";
        private const int _startingNumber = 5;
        private const int _digitCount = 7;


        [TestInitialize]
        public void Setup()
        {
            _loggerMock.Reset();
            _directoryFactoryMock.Reset();
            _directoryFactoryMock.Setup(m => m.Create(_fileLocation)).Returns(_directoryMock.Object);
            _directoryMock.Reset();
            _fileFilterMock.Reset();
            _fileSortMock.Reset();
            _fileRenameMock.Reset();
            _consoleWrapperMock.Reset();
            _testFile1.Reset();
            _testFile2.Reset();
            _testFile3.Reset();
            _testNumbered1.Reset();
            _testNumbered2.Reset();
            _testRenamed1.Reset();
            _testRenamed2.Reset();
        }

        [TestMethod]    
        public void Command_ShouldWritePreviewToConsole()
        {
            IVideoFile[] allFiles = [_testFile1.Object, _testFile2.Object, _testFile3.Object];
            _directoryMock.Setup(m => m.GetFilesInDirectory()).Returns(allFiles);
            IVideoFile[] filtered = [_testFile1.Object, _testFile2.Object];
            _fileFilterMock.Setup(m => m.GetMatchingVideos(allFiles)).Returns(filtered);
            List<INumberedVideoFile> numbered = [_testNumbered1.Object, _testNumbered2.Object];
            _fileSortMock.Setup(m => m.GetOrderedFiles(filtered, _startingNumber)).Returns(numbered);
            SetupRenamedFile(_testRenamed1, "old1", "new1", 1);
            SetupRenamedFile(_testRenamed2, "old2", "new file", 2);
            List<IRenamedVideoFile> renamed = [_testRenamed1.Object, _testRenamed2.Object];
            _fileRenameMock.Setup(m => m.GetRenamedFiles(numbered, _prefix, _suffix, _digitCount)).Returns(renamed);
            var command = new RenameCommand(
                _loggerMock.Object,
                _directoryFactoryMock.Object,
                _fileFilterMock.Object,
                _fileSortMock.Object,
                _fileRenameMock.Object,
                _consoleWrapperMock.Object);

            command.Rename(_fileLocation, _prefix, _suffix, _startingNumber, _digitCount);

            _directoryFactoryMock.Verify(m => m.Create(_fileLocation), Times.Once());
            _directoryMock.Verify(m => m.GetFilesInDirectory(), Times.Once());
            _directoryFactoryMock.VerifyNoOtherCalls();
            _directoryMock.VerifyNoOtherCalls();
            _fileFilterMock.Verify(m => m.GetMatchingVideos(allFiles), Times.Once());
            _fileFilterMock.VerifyNoOtherCalls();
            _fileSortMock.Verify(m => m.GetOrderedFiles(filtered, _startingNumber), Times.Once());
            _fileSortMock.VerifyNoOtherCalls();
            _fileRenameMock.Verify(m => m.GetRenamedFiles(numbered, _prefix, _suffix, _digitCount), Times.Once());
            _fileRenameMock.VerifyNoOtherCalls();
            _consoleWrapperMock.Verify(m => m.WriteLine("1: old1 -> new1"), Times.Once());
            _consoleWrapperMock.Verify(m => m.WriteLine("2: old2 -> new file"), Times.Once());
        }

        [TestMethod]
        public void Command_ShouldNotCommitToDisk_IfDryRun()
        {
            IVideoFile[] allFiles = [_testFile1.Object, _testFile2.Object, _testFile3.Object];
            _directoryMock.Setup(m => m.GetFilesInDirectory()).Returns(allFiles);
            IVideoFile[] filtered = [_testFile1.Object, _testFile2.Object];
            _fileFilterMock.Setup(m => m.GetMatchingVideos(allFiles)).Returns(filtered);
            List<INumberedVideoFile> numbered = [_testNumbered1.Object, _testNumbered2.Object];
            _fileSortMock.Setup(m => m.GetOrderedFiles(filtered, _startingNumber)).Returns(numbered);
            SetupRenamedFile(_testRenamed1, "old1", "new1", 1);
            SetupRenamedFile(_testRenamed2, "old2", "new file", 2);
            List<IRenamedVideoFile> renamed = [_testRenamed1.Object, _testRenamed2.Object];
            _fileRenameMock.Setup(m => m.GetRenamedFiles(numbered, _prefix, _suffix, _digitCount)).Returns(renamed);
            var command = new RenameCommand(
                _loggerMock.Object,
                _directoryFactoryMock.Object,
                _fileFilterMock.Object,
                _fileSortMock.Object,
                _fileRenameMock.Object,
                _consoleWrapperMock.Object);

            command.Rename(_fileLocation, _prefix, _suffix, _startingNumber, _digitCount, true);

            _directoryFactoryMock.Verify(m => m.Create(_fileLocation), Times.Once());
            _directoryMock.Verify(m => m.GetFilesInDirectory(), Times.Once());
            _directoryFactoryMock.VerifyNoOtherCalls();
            _directoryMock.VerifyNoOtherCalls();
            _fileFilterMock.Verify(m => m.GetMatchingVideos(allFiles), Times.Once());
            _fileFilterMock.VerifyNoOtherCalls();
            _fileSortMock.Verify(m => m.GetOrderedFiles(filtered, _startingNumber), Times.Once());
            _fileSortMock.VerifyNoOtherCalls();
            _fileRenameMock.Verify(m => m.GetRenamedFiles(numbered, _prefix, _suffix, _digitCount), Times.Once());
            _fileRenameMock.VerifyNoOtherCalls();
            _testRenamed1.Verify(m => m.CommitRenameToDisk(), Times.Never());
            _testRenamed2.Verify(m => m.CommitRenameToDisk(), Times.Never());
        }

        [TestMethod]
        public void Command_ShouldCommitToDisk_IfNotDryRun()
        {
            IVideoFile[] allFiles = [_testFile1.Object, _testFile2.Object, _testFile3.Object];
            _directoryMock.Setup(m => m.GetFilesInDirectory()).Returns(allFiles);
            IVideoFile[] filtered = [_testFile1.Object, _testFile2.Object];
            _fileFilterMock.Setup(m => m.GetMatchingVideos(allFiles)).Returns(filtered);
            List<INumberedVideoFile> numbered = [_testNumbered1.Object, _testNumbered2.Object];
            _fileSortMock.Setup(m => m.GetOrderedFiles(filtered, _startingNumber)).Returns(numbered);
            SetupRenamedFile(_testRenamed1, "old1", "new1", 1);
            SetupRenamedFile(_testRenamed2, "old2", "new file", 2);
            List<IRenamedVideoFile> renamed = [_testRenamed1.Object, _testRenamed2.Object];
            _fileRenameMock.Setup(m => m.GetRenamedFiles(numbered, _prefix, _suffix, _digitCount)).Returns(renamed);
            var command = new RenameCommand(
                _loggerMock.Object,
                _directoryFactoryMock.Object,
                _fileFilterMock.Object,
                _fileSortMock.Object,
                _fileRenameMock.Object,
                _consoleWrapperMock.Object);

            command.Rename(_fileLocation, _prefix, _suffix, _startingNumber, _digitCount);

            _directoryFactoryMock.Verify(m => m.Create(_fileLocation), Times.Once());
            _directoryMock.Verify(m => m.GetFilesInDirectory(), Times.Once());
            _directoryFactoryMock.VerifyNoOtherCalls();
            _directoryMock.VerifyNoOtherCalls();
            _fileFilterMock.Verify(m => m.GetMatchingVideos(allFiles), Times.Once());
            _fileFilterMock.VerifyNoOtherCalls();
            _fileSortMock.Verify(m => m.GetOrderedFiles(filtered, _startingNumber), Times.Once());
            _fileSortMock.VerifyNoOtherCalls();
            _fileRenameMock.Verify(m => m.GetRenamedFiles(numbered, _prefix, _suffix, _digitCount), Times.Once());
            _fileRenameMock.VerifyNoOtherCalls();
            _testRenamed1.Verify(m => m.CommitRenameToDisk(), Times.Once());
            _testRenamed2.Verify(m => m.CommitRenameToDisk(), Times.Once());
        }

        [TestMethod]
        public void Command_ShouldNotDoAnything_IfNoMatchingFiles()
        {
            IVideoFile[] allFiles = [_testFile1.Object, _testFile2.Object, _testFile3.Object];
            _directoryMock.Setup(m => m.GetFilesInDirectory()).Returns(allFiles);
            _fileFilterMock.Setup(m => m.GetMatchingVideos(allFiles)).Returns([]);
            _fileSortMock.Setup(m => m.GetOrderedFiles(It.Is<IEnumerable<IVideoFile>>(e => !e.Any()), _startingNumber)).Returns([]);
            _fileRenameMock.Setup(m => m.GetRenamedFiles(It.Is<IList<INumberedVideoFile>>(e => !e.Any()), _prefix, _suffix, _digitCount)).Returns([]);
            var command = new RenameCommand(
                _loggerMock.Object,
                _directoryFactoryMock.Object,
                _fileFilterMock.Object,
                _fileSortMock.Object,
                _fileRenameMock.Object,
                _consoleWrapperMock.Object);

            command.Rename(_fileLocation, _prefix, _suffix, _startingNumber, _digitCount, true);

            _directoryFactoryMock.Verify(m => m.Create(_fileLocation), Times.Once());
            _directoryMock.Verify(m => m.GetFilesInDirectory(), Times.Once());
            _directoryFactoryMock.VerifyNoOtherCalls();
            _directoryMock.VerifyNoOtherCalls();
            _fileFilterMock.Verify(m => m.GetMatchingVideos(allFiles), Times.Once());
            _fileFilterMock.VerifyNoOtherCalls();
            _fileSortMock.Verify(m => m.GetOrderedFiles(It.Is<IEnumerable<IVideoFile>>(e => !e.Any()), _startingNumber), Times.Once());
            _fileSortMock.VerifyNoOtherCalls();
            _fileRenameMock.Verify(m => m.GetRenamedFiles(It.Is<IList<INumberedVideoFile>>(e => !e.Any()), _prefix, _suffix, _digitCount), Times.Once());
            _fileRenameMock.VerifyNoOtherCalls();
            _consoleWrapperMock.VerifyNoOtherCalls();
            _testRenamed1.VerifyNoOtherCalls();
            _testRenamed2.VerifyNoOtherCalls();
        }

        private void SetupRenamedFile(Mock<IRenamedVideoFile> file, string oldName, string newName, int order)
        {
            file.Setup(m => m.Name).Returns(oldName);
            file.Setup(m => m.NewName).Returns(newName);
            file.Setup(m => m.NewIndex).Returns(order);
        }
    }
}
