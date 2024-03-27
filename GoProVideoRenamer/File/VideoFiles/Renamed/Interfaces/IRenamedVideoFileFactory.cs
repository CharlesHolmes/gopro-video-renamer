using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;

namespace GoProVideoRenamer.File.VideoFiles.Renamed.Interfaces
{
    public interface IRenamedVideoFileFactory
    {
        IRenamedVideoFile Create(string newName, INumberedVideoFile file);
    }
}