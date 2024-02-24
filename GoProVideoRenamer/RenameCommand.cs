using Cocona;
using GoProVideoRenamer.Directory.Interfaces;
using GoProVideoRenamer.File.Interfaces;
using GoProVideoRenamer.File.Models;
using Microsoft.Extensions.Logging;

namespace GoProVideoRenamer
{
    public class RenameCommand
    {
        private readonly ILogger<RenameCommand> _logger;
        private readonly IVideoDirectoryFactory _directoryFactory;
        private readonly IFileFilter _filenameFilter;
        private readonly IFileSort _fileSort;
        private readonly IFileRename _fileRename;
        private readonly IConsoleWrapper _console;

        public RenameCommand(
            ILogger<RenameCommand> logger,
            IVideoDirectoryFactory directoryFactory,
            IFileFilter filenameFilter,
            IFileSort fileSort,
            IFileRename fileRename,
            IConsoleWrapper console)
        {
            _logger = logger;
            _directoryFactory = directoryFactory;
            _filenameFilter = filenameFilter;
            _fileSort = fileSort;
            _fileRename = fileRename;
            _console = console;
        }

        public void Rename(
            [Option(Description = "Directory where the GoPro videos are stored")] string fileLocation,
            [Option(Description = "Text that should appear before each file's number")] string? prefix,
            [Option(Description = "Text that should appear after each file's number")] string? suffix,
            [Option(Description = "What number the renamed files should start at")] int? startingNumber,
            [Option(Description = "The number of digits to include in each file number")] int? digitCount,
            [Option(Description = "Print a list of the files to be renamed, but do not rename them")] bool dryRun = false)
        {
            var videoDirectory = _directoryFactory.Create(fileLocation);
            var allFilesInDirectory = videoDirectory.GetFilesInDirectory();
            var matchingVideoFiles = _filenameFilter.GetMatchingVideos(allFilesInDirectory);
            var sortedVideoFiles = _fileSort.GetOrderedFiles(matchingVideoFiles, startingNumber);
            var renamedFiles = _fileRename.GetRenamedFiles(sortedVideoFiles, prefix, suffix, digitCount);
            foreach (RenamedVideoFile file in renamedFiles)
            {
                _console.WriteLine($"{file.NewIndex}: {file.Name} -> {file.NewName}");
                if (!dryRun)
                {
                    file.CommitRenameToDisk();
                }
            }
        }
    }
}
