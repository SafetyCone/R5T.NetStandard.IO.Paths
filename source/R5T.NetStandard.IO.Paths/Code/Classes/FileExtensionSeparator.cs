using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Separates a file-name without file-extension from the file-extension.
    /// </summary>
    public class FileExtensionSeparator : FileNameSegmentSeparator
    {
        #region Static

        /// <summary>
        /// The <see cref="Constants.DefaultFileExtensionSeparator"/> file-extension separator.
        /// </summary>
        /// <remarks>
        /// Use of 'new', even though value is the same!
        /// </remarks>
        public static readonly new FileExtensionSeparator Default = new FileExtensionSeparator(Constants.DefaultFileExtensionSeparator);

        #endregion


        public FileExtensionSeparator(string value)
            : base(value)
        {
        }
    }
}
