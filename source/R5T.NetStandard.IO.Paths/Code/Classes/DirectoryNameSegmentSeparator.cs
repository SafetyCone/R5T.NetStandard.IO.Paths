using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Separates <see cref="DirectoryNameSegment"/>s.
    /// </summary>
    public class DirectoryNameSegmentSeparator : TypedString
    {
        #region Static

        /// <summary>
        /// The <see cref="Constants.DefaultDirectoryNameSegmentSeparator"/> directory-name segment separator.
        /// </summary>
        public static readonly DirectoryNameSegmentSeparator Default = new DirectoryNameSegmentSeparator(Constants.DefaultDirectoryNameSegmentSeparator);

        #endregion


        public DirectoryNameSegmentSeparator(string value)
            : base(value)
        {
        }
    }
}
