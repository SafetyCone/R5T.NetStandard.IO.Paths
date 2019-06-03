using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Separates paths in path aggregation (like the PATH environment variable). For example, the ';' in "path1;path2;path3".
    /// </summary>
    public class PathSeparator : TypedString
    {
        #region Static

        /// <summary>
        /// The default <see cref="Constants.DefaultPathSeparator"/> value.
        /// </summary>
        public static readonly PathSeparator Default = new PathSeparator(Constants.DefaultPathSeparator);

        #endregion



        public PathSeparator(string value)
            : base(value)
        {
        }
    }
}
