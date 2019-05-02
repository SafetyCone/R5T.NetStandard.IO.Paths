using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// An absolute (or rooted) file path. Allows communicating the distinction between file and directory paths.
    /// Note the absolute path type is the default path type, in contrast to the relative path type. Thus there is no need for an "AbsoluteFilePath" type.
    /// </summary>
    public class FilePath : AbsolutePath
    {
        public FilePath()
        {
        }

        public FilePath(string value)
            : base(value)
        {
        }
    }
}
