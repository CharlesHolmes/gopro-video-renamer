using GoProVideoRenamer.File.Models;

namespace GoProVideoRenamer.File.Interfaces
{
    public interface IFileSort
    {
        List<NumberedVideoFile> GetOrderedFiles(IEnumerable<VideoFile> files, int? startingNumber);
    }
}