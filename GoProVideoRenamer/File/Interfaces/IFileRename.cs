using GoProVideoRenamer.File.Models;

namespace GoProVideoRenamer.File.Interfaces
{
    public interface IFileRename
    {
        IList<RenamedVideoFile> GetRenamedFiles(
            IList<NumberedVideoFile> files,
            string? prefix,
            string? suffix,
            int? digitCount);
    }
}