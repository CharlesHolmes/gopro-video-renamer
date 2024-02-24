using GoProVideoRenamer.File.Models;

namespace GoProVideoRenamer.File.Interfaces
{
    public interface IFileFilter
    {
        IEnumerable<VideoFile> GetMatchingVideos(IEnumerable<VideoFile> allFiles);
    }
}