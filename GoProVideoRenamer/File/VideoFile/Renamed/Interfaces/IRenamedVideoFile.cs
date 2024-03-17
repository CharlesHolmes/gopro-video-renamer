using GoProVideoRenamer.File.VideoFile.Numbered.Interfaces;

namespace GoProVideoRenamer.File.VideoFile.Renamed.Interfaces
{
    public interface IRenamedVideoFile : INumberedVideoFile
    {
        string NewName { get; }

        void CommitRenameToDisk();
    }
}