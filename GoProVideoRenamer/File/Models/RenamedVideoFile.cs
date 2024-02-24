namespace GoProVideoRenamer.File.Models
{
    public class RenamedVideoFile : NumberedVideoFile
    {
        public string NewName { get; private init; }

        public RenamedVideoFile(string newName, NumberedVideoFile file) : base(file)
        {
            NewName = newName;
        }

        public void CommitRenameToDisk()
        {
            _fileInfo.MoveTo($"{_fileInfo.Directory!.FullName}\\{NewName}");
        }
    }
}
