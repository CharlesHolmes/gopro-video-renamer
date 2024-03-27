using GoProVideoRenamer.File.VideoFiles.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.File.VideoFiles.Numbered
{
    public class NumberedVideoFileFactory : INumberedVideoFileFactory
    {
        public INumberedVideoFile Create(int newIndex, IVideoFile file) => new NumberedVideoFile(newIndex, file);
    }
}
