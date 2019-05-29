using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// A relative (or non-rooted) directory path that is relative to a different absolute path (file or directory).
    /// </summary>
    public class DirectoryRelativePath : DirectoryPathSegment
    {
        public DirectoryRelativePath(string value)
            : base(value)
        {
        }
    }
}
