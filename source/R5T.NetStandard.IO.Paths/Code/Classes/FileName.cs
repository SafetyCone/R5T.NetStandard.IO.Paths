using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// A file-name.
    /// Note the file-name with extension type is the default file-name type, in contrast to the file-name with extension file-name type. Thus there is no need for a "FileNameWithExtension" type.
    /// </summary>
    public class FileName : FilePathSegment
    {
        public FileName()
        {
        }

        public FileName(string value)
            : base(value)
        {
        }
    }
}
