using System.IO.Abstractions;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;

namespace GoProVideoRenamer.File.VideoFiles.Numbered
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
