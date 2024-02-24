using System.IO.Abstractions;

namespace GoProVideoRenamer.File.Models
{
    public class NumberedVideoFile : VideoFile
    {
        public int NewIndex { get; private init; }

        public NumberedVideoFile(int newIndex, VideoFile file) : base(file)
        {
            NewIndex = newIndex;
        }

        public NumberedVideoFile(NumberedVideoFile other) : this(other.NewIndex, other) { }
    }
}
