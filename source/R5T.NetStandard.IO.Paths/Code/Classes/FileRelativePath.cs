using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// A relative (or non-rooted) file path that is relative to a different absolute path (file or directory).
    /// </summary>
    public class FileRelativePath : FilePathSegment
    {
        public FileRelativePath(string value)
            : base(value)
        {
        }
    }
}
