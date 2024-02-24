using GoProVideoRenamer.File.Interfaces;
using GoProVideoRenamer.File.Models;

namespace GoProVideoRenamer.File
{
    public class FileSort : IFileSort
    {
        // https://community.gopro.com/s/article/GoPro-Camera-File-Naming-Convention?language=en_US
        public List<NumberedVideoFile> GetOrderedFiles(IEnumerable<VideoFile> files, int? startingNumber)
        {
            var filesByNumber = new SortedList<int, List<VideoFile>>();
            foreach (VideoFile file in files)
            {
                if (!filesByNumber.ContainsKey(file.FileNumber))
                {
                    filesByNumber.Add(file.FileNumber, new List<VideoFile>());
                }

                filesByNumber[file.FileNumber].Add(file);
            }

            foreach (int fileNumber in filesByNumber.Keys)
            {
                filesByNumber[fileNumber].Sort((x, y) => x.ChapterNumber - y.ChapterNumber);
            }

            int firstNumber = startingNumber.HasValue ? startingNumber.Value : 1;
            return filesByNumber.Values
                .Aggregate(Enumerable.Empty<VideoFile>(), (x, y) => x.Concat(y))
                .Select((file, i) => new NumberedVideoFile(i + firstNumber, file))
                .ToList();
        }
    }
}
