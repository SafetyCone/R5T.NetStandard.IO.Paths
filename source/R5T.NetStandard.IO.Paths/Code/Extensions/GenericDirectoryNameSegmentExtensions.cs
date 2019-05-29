using System;


namespace R5T.NetStandard.IO.Paths
{
    public static class GenericDirectoryNameSegmentExtensions
    {
        public static DirectoryName ToDirectoryName(this GenericDirectoryNameSegment directoryNameSegment)
        {
            var directoryName = new DirectoryName(directoryNameSegment.Value);
            return directoryName;
        }
    }
}
