using GoProVideoRenamer.File.VideoFile;
using GoProVideoRenamer.File.VideoFile.Interfaces;
using GoProVideoRenamer.File.VideoFile.Numbered.Interfaces;

namespace GoProVideoRenamer.File.Interfaces
{
    public interface IFileSort
    {
        List<INumberedVideoFile> GetOrderedFiles(IEnumerable<IVideoFile> files, int? startingNumber);
    }
}