using GoProVideoRenamer.File.VideoFile.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFile.Renamed.Interfaces;

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