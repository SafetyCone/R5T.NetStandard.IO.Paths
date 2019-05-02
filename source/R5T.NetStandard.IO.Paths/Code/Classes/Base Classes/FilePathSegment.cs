using System;


namespace R5T.NetStandard.IO.Paths 
{
    /// <summary>
    /// File paths are made of file path segments.
    /// </summary>
    public abstract class FilePathSegment : PathSegment
    {
        public FilePathSegment()
        {
        }

        public FilePathSegment(string value)
            : base(value)
        {
        }
    }
}
