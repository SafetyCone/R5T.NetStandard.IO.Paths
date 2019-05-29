using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Directory paths are made of directory path segments.
    /// </summary>
    public abstract class DirectoryPathSegment : PathSegment
    {
        public DirectoryPathSegment(string value)
            : base(value)
        {
        }
    }
}
