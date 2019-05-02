using System;


namespace R5T.NetStandard.IO.Paths
{
    public class FileNameWithoutExtension : TypedString
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
