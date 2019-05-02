using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// An absolute (or rooted) directory path. Allows communicating the distinction between file and directory paths.
    /// Note the absolute path type is the default path type, in contrast to the relative path type. Thus there is no need for an "AbsoluteDirectoryPath" type.
    /// </summary>
    public class DirectoryPath : AbsolutePath
    {
        public DirectoryPath()
        {
        }

        public DirectoryPath(string value)
            : base(value)
        {
        }
    }
}
