using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// A name of a directory, as a strongly-typed string.
    /// </summary>
    public class DirectoryName : DirectoryPathSegment
    {
        public DirectoryName()
        {
        }

        public DirectoryName(string value)
            : base(value)
        {
        }
    }
}
