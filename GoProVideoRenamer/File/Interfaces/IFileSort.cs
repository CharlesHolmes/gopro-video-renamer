using GoProVideoRenamer.File.VideoFiles;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;

namespace GoProVideoRenamer.File.Interfaces
{
    public interface IFileSort
    {
        List<INumberedVideoFile> GetOrderedFiles(IEnumerable<IVideoFile> files, int? startingNumber);
    }
}