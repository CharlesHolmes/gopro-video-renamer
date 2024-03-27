using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;

namespace GoProVideoRenamer.File.VideoFiles.Renamed.Interfaces
{
    public interface IRenamedVideoFile : INumberedVideoFile
    {
        string NewName { get; }

        void CommitRenameToDisk();
    }
}