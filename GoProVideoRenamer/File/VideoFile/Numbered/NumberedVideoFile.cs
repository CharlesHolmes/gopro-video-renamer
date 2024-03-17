using System.IO.Abstractions;
using GoProVideoRenamer.File.VideoFile.Interfaces;
using GoProVideoRenamer.File.VideoFile.Numbered.Interfaces;

namespace GoProVideoRenamer.File.VideoFile.Numbered
{
    public class NumberedVideoFile : VideoFile, INumberedVideoFile
    {
        public int NewIndex { get; private init; }

        public NumberedVideoFile(int newIndex, IVideoFile file) : base(file)
        {
            NewIndex = newIndex;
        }

        public NumberedVideoFile(INumberedVideoFile other) : this(other.NewIndex, other) { }
    }
}
