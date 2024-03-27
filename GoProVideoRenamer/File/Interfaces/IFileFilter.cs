using GoProVideoRenamer.File.VideoFiles.Interfaces;

namespace GoProVideoRenamer.File.Interfaces
{
    public interface IFileFilter
    {
        IEnumerable<IVideoFile> GetMatchingVideos(IEnumerable<IVideoFile> allFiles);
    }
}