using GoProVideoRenamer.File.VideoFile;
using GoProVideoRenamer.File.VideoFile.Interfaces;

namespace GoProVideoRenamer.Directory.Interfaces
{
    public interface IVideoDirectory
    {
        IEnumerable<IVideoFile> GetFilesInDirectory();
    }
}