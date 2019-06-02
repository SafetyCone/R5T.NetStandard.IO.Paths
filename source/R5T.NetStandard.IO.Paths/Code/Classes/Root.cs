using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// A root of a path (drive name).
    /// </summary>
    public class Root : AbsolutePath
    {
        public Root(string value)
            : base(value)
        {
        }
    }
}
