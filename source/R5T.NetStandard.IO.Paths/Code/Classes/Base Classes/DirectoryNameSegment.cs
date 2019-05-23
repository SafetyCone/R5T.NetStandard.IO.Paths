using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// A piece of a directory name.
    /// Directory names are composed of directory name segments.
    /// </summary>
    public abstract class DirectoryNameSegment : TypedString
    {
        public DirectoryNameSegment()
        {
        }

        public DirectoryNameSegment(string value)
            : base(value)
        {
        }
    }
}
