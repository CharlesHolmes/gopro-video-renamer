using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Renamed.Interfaces;

namespace GoProVideoRenamer.File.Interfaces
{
    public interface IFileRename
    {
        IList<IRenamedVideoFile> GetRenamedFiles(
            IList<INumberedVideoFile> files,
            string? prefix,
            string? suffix,
            int? digitCount);
    }
}