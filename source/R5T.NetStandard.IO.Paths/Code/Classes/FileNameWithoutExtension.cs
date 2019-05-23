using System;


namespace R5T.NetStandard.IO.Paths
{
    public class FileNameWithoutExtension : FileNameSegment
    {
        public FileNameWithoutExtension()
        {
        }

        public FileNameWithoutExtension(string value)
            : base(value)
        {
        }
    }
}
