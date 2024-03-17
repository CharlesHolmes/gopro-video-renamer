using GoProVideoRenamer.File.VideoFile.Interfaces;

namespace GoProVideoRenamer.File.VideoFile.Numbered.Interfaces
{
    public interface INumberedVideoFileFactory
    {
        INumberedVideoFile Create(int newIndex, IVideoFile file);
    }
}