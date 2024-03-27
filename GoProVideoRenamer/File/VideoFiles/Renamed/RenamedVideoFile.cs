using GoProVideoRenamer.File.VideoFiles.Numbered;
using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Renamed.Interfaces;

namespace GoProVideoRenamer.File.VideoFiles.Renamed
{
    public class RenamedVideoFile : NumberedVideoFile, IRenamedVideoFile
    {
        public string NewName { get; private init; }

        public RenamedVideoFile(string newName, INumberedVideoFile file) : base(file)
        {
            NewName = newName;
        }

        public void CommitRenameToDisk()
        {
            FileInfo.MoveTo($"{FileInfo.Directory!.FullName}\\{NewName}");
        }
    }
}
