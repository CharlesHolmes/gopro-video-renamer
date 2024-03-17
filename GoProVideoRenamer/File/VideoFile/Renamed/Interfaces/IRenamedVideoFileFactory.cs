using GoProVideoRenamer.File.VideoFile.Numbered.Interfaces;

namespace GoProVideoRenamer.File.VideoFile.Renamed.Interfaces
{
    public interface IRenamedVideoFileFactory
    {
        IRenamedVideoFile Create(string newName, INumberedVideoFile file);
    }
}