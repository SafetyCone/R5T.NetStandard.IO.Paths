using System;


namespace R5T.NetStandard.IO.Paths
{
    public static class GenericFileNameSegmentExtensions
    {
        public static FileName ToFileName(this GenericFileNameSegment fileNameSegment)
        {
            var fileName = new FileName(fileNameSegment.Value);
            return fileName;
        }

        public static FileNameWithoutExtension ToFileNameWithoutExtension(this GenericFileNameSegment fileNameSegment)
        {
            var fileName = new FileNameWithoutExtension(fileNameSegment.Value);
            return fileName;
        }
    }
}
