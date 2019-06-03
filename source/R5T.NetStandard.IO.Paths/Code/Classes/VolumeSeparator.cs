using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Separates the volume (or root) from the rest of the path. For example, the ':' in "C:\Temp\temp.txt".
    /// </summary>
    public class VolumeSeparator : TypedString
    {
        #region Static

        /// <summary>
        /// The default <see cref="Constants.DefaultVolumeSeparator"/> value.
        /// </summary>
        public static readonly VolumeSeparator Default = new VolumeSeparator(Constants.DefaultVolumeSeparator);

        #endregion


        public VolumeSeparator(string value)
            : base(value)
        {
        }
    }
}
