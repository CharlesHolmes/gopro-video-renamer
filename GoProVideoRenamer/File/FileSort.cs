using GoProVideoRenamer.File.Interfaces;
using GoProVideoRenamer.File.VideoFiles;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;

namespace GoProVideoRenamer.File
{
    public class FileSort : IFileSort
    {
        private readonly INumberedVideoFileFactory _numberedVideoFileFactory;

        public FileSort(INumberedVideoFileFactory numberedVideoFileFactory)
        {
            _numberedVideoFileFactory = numberedVideoFileFactory;
        }

        // https://community.gopro.com/s/article/GoPro-Camera-File-Naming-Convention?language=en_US
        public List<INumberedVideoFile> GetOrderedFiles(IEnumerable<IVideoFile> files, int? startingNumber)
        {
            VerifyNoDuplicates(files);
            var filesByNumber = new SortedList<int, List<IVideoFile>>();
            foreach (IVideoFile file in files)
            {
                if (!filesByNumber.ContainsKey(file.FileNumber))
                {
                    filesByNumber.Add(file.FileNumber, new List<IVideoFile>());
                }

                filesByNumber[file.FileNumber].Add(file);
            }

            foreach (int fileNumber in filesByNumber.Keys)
            {
                filesByNumber[fileNumber].Sort((x, y) => x.ChapterNumber - y.ChapterNumber);
            }

            int firstNumber = startingNumber.HasValue ? startingNumber.Value : 1;
            return filesByNumber.Values
                .Aggregate(Enumerable.Empty<IVideoFile>(), (x, y) => x.Concat(y))
                .Select((file, i) => _numberedVideoFileFactory.Create(i + firstNumber, file))
                .ToList();
        }

        private struct FileIdentifier
        {
            public int SerialNumber;
            public int ChapterNumber;

            public FileIdentifier(IVideoFile file)
            {
                SerialNumber = file.FileNumber;
                ChapterNumber = file.ChapterNumber;
            }
        }

        private void VerifyNoDuplicates(IEnumerable<IVideoFile> files)
        {
            var filesById = new HashSet<FileIdentifier>();
            foreach (FileIdentifier id in files.Select(f => new FileIdentifier(f))) 
            {
                if (filesById.Contains(id))
                {
                    throw new ArgumentException("Duplicate files detected in input; cannot proceed");
                }
                else
                {
                    filesById.Add(id);
                }
            }
        }
    }
}
