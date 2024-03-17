using GoProVideoRenamer.File.VideoFile.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFile.Renamed.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.File.VideoFile.Renamed
{
    public class RenamedVideoFileFactory : IRenamedVideoFileFactory
    {
        public IRenamedVideoFile Create(string newName, INumberedVideoFile file) => new RenamedVideoFile(newName, file);
    }
}
