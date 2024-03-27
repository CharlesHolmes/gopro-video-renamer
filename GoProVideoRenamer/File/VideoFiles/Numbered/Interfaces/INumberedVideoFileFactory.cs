using GoProVideoRenamer.File.VideoFiles.Interfaces;

namespace GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces
{
    public interface INumberedVideoFileFactory
    {
        INumberedVideoFile Create(int newIndex, IVideoFile file);
    }
}