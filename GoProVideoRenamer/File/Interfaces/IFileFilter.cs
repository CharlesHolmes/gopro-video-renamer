using GoProVideoRenamer.File.VideoFile.Interfaces;

namespace GoProVideoRenamer.File.Interfaces
{
    public interface IFileFilter
    {
        IEnumerable<IVideoFile> GetMatchingVideos(IEnumerable<IVideoFile> allFiles);
    }
}