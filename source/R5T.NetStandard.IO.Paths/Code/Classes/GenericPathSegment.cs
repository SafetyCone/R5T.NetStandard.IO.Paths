using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Usually path segments will be strongly-typed to communicate some specific piece of informaiton and derive from <see cref="PathSegment"/> (<see cref="FilePathSegment"/> and <see cref="DirectoryPathSegment"/>).
    /// But a client may want to create segmented paths without deriving their own path segment type. This generic path segment type allows that.
    /// </summary>
    /// <remarks>
    /// The class is sealed. To create your own path segment type, derive from <see cref="PathSegment"/>.
    /// Note, there is no string extension-method to convert a string into a <see cref="GenericPathSegment"/> to emphasize that the <see cref="GenericPathSegment"/> should be used sparingly.
    /// </remarks>
    public class GenericPathSegment : PathSegment
    {
        public GenericPathSegment(string value)
            : base(value)
        {
        }
    }
}
