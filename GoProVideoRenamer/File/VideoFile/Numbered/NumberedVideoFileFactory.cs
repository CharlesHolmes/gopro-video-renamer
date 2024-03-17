using GoProVideoRenamer.File.VideoFile.Interfaces;
using GoProVideoRenamer.File.VideoFile.Numbered.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.File.VideoFile.Numbered
{
    public class NumberedVideoFileFactory : INumberedVideoFileFactory
    {
        public INumberedVideoFile Create(int newIndex, IVideoFile file) => new NumberedVideoFile(newIndex, file);
    }
}
