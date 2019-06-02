using System;


namespace R5T.NetStandard.IO.Paths
{
    public static class GenericDirectoryNameSegmentExtensions
    {
        public static DirectoryName AsDirectoryName(this GenericDirectoryNameSegment directoryNameSegment)
        {
            var directoryName = new DirectoryName(directoryNameSegment.Value);
            return directoryName;
        }
    }
}
