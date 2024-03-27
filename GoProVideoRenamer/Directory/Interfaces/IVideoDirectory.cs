using GoProVideoRenamer.File.VideoFiles;
using GoProVideoRenamer.File.VideoFiles.Interfaces;

namespace GoProVideoRenamer.Directory.Interfaces
{
    public interface IVideoDirectory
    {
        IEnumerable<IVideoFile> GetFilesInDirectory();
    }
}