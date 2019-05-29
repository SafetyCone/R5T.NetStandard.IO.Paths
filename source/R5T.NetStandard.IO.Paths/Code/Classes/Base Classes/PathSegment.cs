using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// A piece of a path, either file path or directory path.
    /// Paths are composed of path segments.
    /// </summary>
    public abstract class PathSegment : TypedString
    {
        public PathSegment(string value)
            : base(value)
        {
        }
    }
}
