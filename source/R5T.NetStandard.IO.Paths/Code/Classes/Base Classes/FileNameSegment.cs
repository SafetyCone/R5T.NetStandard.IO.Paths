using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// A piece of a file name.
    /// File names are composed of file name segments. Usually, there is just a <see cref="FileNameWithoutExtension"/> and a <see cref="FileExtension"/>.
    /// But the <see cref="FileNameWithoutExtension"/> could itself be composed of mulitple <see cref="FileNameSegment"/>s.
    /// </summary>
    public abstract class FileNameSegment : TypedString
    {
        public FileNameSegment(string value)
            : base(value)
        {
        }
    }
}
