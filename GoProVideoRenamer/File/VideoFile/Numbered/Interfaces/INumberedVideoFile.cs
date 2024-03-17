using GoProVideoRenamer.File.VideoFile.Interfaces;

namespace GoProVideoRenamer.File.VideoFile.Numbered.Interfaces
{
    public interface INumberedVideoFile : IVideoFile
    {
        int NewIndex { get; }
    }
}