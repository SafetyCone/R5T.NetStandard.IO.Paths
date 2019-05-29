using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// A name of a directory, as a strongly-typed string.
    /// </summary>
    public class DirectoryName : DirectoryPathSegment
    {
        #region Static

        /// <summary>
        /// The <see cref="Constants.DefaultCurrentDirectoryName"/> directory-name.
        /// </summary>
        public static readonly DirectoryName Current = new DirectoryName(Constants.DefaultCurrentDirectoryName);
        /// <summary>
        /// The <see cref="Constants.DefaultParentDirectoryName"/> directory-name.
        /// </summary>
        public static readonly DirectoryName Parent = new DirectoryName(Constants.DefaultParentDirectoryName);

        #endregion


        public DirectoryName(string value)
            : base(value)
        {
        }
    }
}
