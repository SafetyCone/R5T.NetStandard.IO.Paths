using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Separates <see cref="FileNameSegment"/>s. For example, the derived <see cref="FileExtensionSeparator"/> separates the <see cref="FileNameWithoutExtension"/> <see cref="FileNameSegment"/> from the <see cref="FileExtension"/> <see cref="FileNameSegment"/>.
    /// </summary>
    public class FileNameSegmentSeparator : TypedString
    {
        #region Static

        /// <summary>
        /// The <see cref="Constants.DefaultFileNameSegmentSeparator"/> file-name segment separator.
        /// </summary>
        public static readonly FileNameSegmentSeparator Default = new FileNameSegmentSeparator(Constants.DefaultFileNameSegmentSeparator);

        #endregion


        public FileNameSegmentSeparator(string value)
            : base(value)
        {
        }
    }
}
