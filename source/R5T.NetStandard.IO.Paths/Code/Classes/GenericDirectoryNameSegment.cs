using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Usually directory name segments will be strongly-typed to communicate some specific piece of informaiton and derive from <see cref="DirectoryNameSegment"/>.
    /// But a client may want to create segmented directory names without deriving their own directory name segment type. This generic directory name segment type allows that.
    /// </summary>
    /// <remarks>
    /// The class is sealed. To create your own directory name segment type, derive from <see cref="DirectoryNameSegment"/>.
    /// Note, there is no string extension-method to convert a string into a <see cref="GenericDirectoryNameSegment"/> to emphasize that the <see cref="GenericDirectoryNameSegment"/> should be used sparingly.
    /// </remarks>
    public sealed class GenericDirectoryNameSegment : DirectoryNameSegment
    {
        public GenericDirectoryNameSegment(string value)
            : base(value)
        {
        }
    }
}
