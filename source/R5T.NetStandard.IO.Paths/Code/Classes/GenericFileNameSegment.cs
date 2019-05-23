using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Usually file name segments will be strongly-typed to communicate some specific piece of informaiton and derive from <see cref="FileNameSegment"/> (<see cref="FileNameWithoutExtension"/> and <see cref="FileExtension"/>).
    /// But a client may want to create segmented file names without deriving their own file name segment type. This generic file name segment type allows that.
    /// </summary>
    /// <remarks>
    /// The class is sealed. To create your own file name segment type, derive from <see cref="FileNameSegment"/>.
    /// </remarks>
    public sealed class GenericFileNameSegment : FileNameSegment
    {
        public GenericFileNameSegment()
        {
        }

        public GenericFileNameSegment(string value)
            : base(value)
        {
        }
    }
}
