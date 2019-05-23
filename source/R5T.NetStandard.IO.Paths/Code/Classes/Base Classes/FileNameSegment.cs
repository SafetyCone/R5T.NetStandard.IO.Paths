using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// A piece of a file name.
    /// File names are composed of file name segments.
    /// </summary>
    public abstract class FileNameSegment : TypedString
    {
        public FileNameSegment()
        {
        }

        public FileNameSegment(string value)
            : base(value)
        {
        }
    }
}
