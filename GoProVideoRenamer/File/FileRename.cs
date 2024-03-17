using GoProVideoRenamer.File.Interfaces;
using GoProVideoRenamer.File.VideoFile;
using GoProVideoRenamer.File.VideoFile.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFile.Renamed.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoProVideoRenamer.File
{
    public class FileRename : IFileRename
    {
        private readonly ILogger<FileRename> _logger;
        private readonly IRenamedVideoFileFactory _renamedVideoFileFactory;

        public FileRename(
            ILogger<FileRename> logger,
            IRenamedVideoFileFactory renamedVideoFileFactory)
        {
            _logger = logger;
            _renamedVideoFileFactory = renamedVideoFileFactory;
        }

        public IList<IRenamedVideoFile> GetRenamedFiles(
            IList<INumberedVideoFile> files,
            string? prefix,
            string? suffix,
            int? digitCount)
        {
            int maxNewIndex = files.Last().NewIndex;
            VerifyDigitCountIsLargeEnough(digitCount, maxNewIndex);
            return files
                .Select(file =>
                    _renamedVideoFileFactory.Create(
                        GetNewFilename(prefix, suffix, GetDigitCount(digitCount, maxNewIndex), file),
                        file))
                .ToList();
        }

        private int GetDigitCount(int? specifiedDigitCount, int maxNewIndex)
        {
            if (specifiedDigitCount.HasValue)
            {
                return specifiedDigitCount.Value;
            }
            else
            {
                return (int)Math.Floor(Math.Log10(maxNewIndex)) + 1;
            }
        }

        private string GetNewFilename(string? prefix, string? suffix, int digitCount, INumberedVideoFile file)
        {
            return $"{prefix}{file.NewIndex.ToString($"D{digitCount}")}{suffix}{file.FileExtension}";
        }

        private void VerifyDigitCountIsLargeEnough(int? digitCount, int maxFileIndex)
        {
            if (digitCount.HasValue)
            {
                _logger.LogInformation("Verifying that digit count is large enough to accommodate maximum file index...");
                int maxFileIndexDigits = (int)Math.Floor(Math.Log10(maxFileIndex)) + 1;
                if (maxFileIndexDigits > digitCount.Value)
                {
                    _logger.LogCritical("Cannot use provided digit count {digitCount} because maximum file index digits {maxDigits} is greater.", digitCount, maxFileIndexDigits);
                    throw new ArgumentOutOfRangeException(nameof(digitCount), $"Digit count must be greater than number of digits in the renamed file index, which is {maxFileIndexDigits}");
                }
                else
                {
                    _logger.LogInformation("Verified that digit count is large enough to accommodate maximum file index.");
                }
            }
        }
    }
}
