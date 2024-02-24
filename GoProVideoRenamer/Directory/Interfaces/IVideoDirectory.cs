using GoProVideoRenamer.File.Models;

namespace GoProVideoRenamer.Directory.Interfaces
{
    public interface IVideoDirectory
    {
        IEnumerable<VideoFile> GetFilesInDirectory();
    }
}