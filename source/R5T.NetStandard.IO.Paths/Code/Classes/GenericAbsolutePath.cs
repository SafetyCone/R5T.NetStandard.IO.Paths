using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Usually absolute paths will be strongly-typed to communicate some specific piece of information and derive from <see cref="AbsolutePath"/>.
    /// But a client may want to create absolute paths without deriving their own absolute path type. This generic absolute path type allows that.
    /// </summary>
    /// <remarks>
    /// The class is sealed. To create your own absolute path type, derive from <see cref="AbsolutePath"/>.
    /// </remarks>
    public sealed class GenericAbsolutePath : AbsolutePath
    {
        public GenericAbsolutePath(string value)
            : base(value)
        {
        }
    }
}
