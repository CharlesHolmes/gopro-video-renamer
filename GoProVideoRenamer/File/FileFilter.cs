using System.Text.RegularExpressions;
using GoProVideoRenamer.File.Interfaces;
using GoProVideoRenamer.File.Models;

namespace GoProVideoRenamer.File
{
    public class FileFilter : IFileFilter
    {
        private const string MATCHING_FILE_PATTERN = "G(H|X)\\d{6}.mp4";
        private readonly Regex _matchingFileRegex = new Regex(MATCHING_FILE_PATTERN, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public IEnumerable<VideoFile> GetMatchingVideos(IEnumerable<VideoFile> allFiles)
        {
            return allFiles.Where(file => _matchingFileRegex.IsMatch(file.Name));
        }
    }
}
