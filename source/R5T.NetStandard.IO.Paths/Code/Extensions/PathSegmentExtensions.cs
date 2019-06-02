using System;


namespace R5T.NetStandard.IO.Paths
{
    public static class PathSegmentExtensions
    {
        public static DirectoryRelativePath AsDirectoryRelativePath(this PathSegment pathSegment)
        {
            var directoryRelativePath = new DirectoryRelativePath(pathSegment.Value);
            return directoryRelativePath;
        }
    }
}
