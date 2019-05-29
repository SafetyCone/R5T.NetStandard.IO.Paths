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
        public static readonly FileExtensionSeparator Default = new FileExtensionSeparator(Constants.DefaultFileExtensionSeparator);

        #endregion


        public FileExtensionSeparator(string value)
            : base(value)
        {
        }
    }
}
