using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// An absolute (i.e. rooted) path for either a directory or path.
    /// In contrast to a relative path, which requires an absolute path to which it is relative in order to be resolvable, an absolute path is directly resolvable.
    /// The <see cref="AbsolutePath"/> class is abstract. Use either <see cref="DirectoryPath"/> or <see cref="FilePath"/> to instantiate actual paths.
    /// </summary>
    public abstract class AbsolutePath : TypedString
    {
        public AbsolutePath()
        {
        }

        public AbsolutePath(string value)
            : base(value)
        {
        }
    }
}
