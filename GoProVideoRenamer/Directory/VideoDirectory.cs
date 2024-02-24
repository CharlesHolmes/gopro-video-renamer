using GoProVideoRenamer.Directory.Interfaces;
using GoProVideoRenamer.File.Models;
using Microsoft.Extensions.Logging;
using System.IO.Abstractions;

namespace GoProVideoRenamer.Directory
{
    public class VideoDirectory : IVideoDirectory
    {
        private readonly ILogger<VideoDirectory> _logger;
        private readonly IFileSystem _fileSystem;
        private readonly string _directoryPath;
        private readonly IDirectoryInfo _directoryInfo;

        public VideoDirectory(
            ILogger<VideoDirectory> logger,
            IFileSystem fileSystem,
            string directoryPath)
        {
            _logger = logger;
            _fileSystem = fileSystem;
            _directoryPath = directoryPath;
            _directoryInfo = _fileSystem.DirectoryInfo.New(_directoryPath);
            VerifyDirectoryExists();
        }

        private void VerifyDirectoryExists()
        {
            _logger.LogInformation("Checking for existence of directory at path {path}", _directoryPath);
            if (!_directoryInfo.Exists)
            {
                _logger.LogCritical("Directory at path {path} does not exist!", _directoryPath);
                throw new DirectoryNotFoundException("Unable to find specified source directory.");
            }
            else
            {
                _logger.LogInformation("Directory at path {path} does exist.", _directoryPath);
            }
        }

        public IEnumerable<VideoFile> GetFilesInDirectory() => _directoryInfo.GetFiles().Select(file => new VideoFile(file));
    }
}
