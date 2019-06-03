﻿using System;

using R5T.NetStandard.OS;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Separates <see cref="PathSegment"/>s (usually directory names and the file name) in a path.
    /// </summary>
    public class DirectorySeparator : TypedString
    {
        #region Static

        /// <summary>
        /// Separates directory path segments in Windows-style paths.
        /// </summary>
        public static readonly DirectorySeparator DefaultWindows = new DirectorySeparator(Constants.DefaultWindowsDirectorySeparator);
        /// <summary>
        /// Separates directory path segments in non-Windows-style paths.
        /// </summary>
        public static readonly DirectorySeparator DefaultNonWindows = new DirectorySeparator(Constants.DefaultNonWindowsDirectorySeparator);
        /// <summary>
        /// Provides the default directory separator on the currently executing platform.
        /// </summary>
        public static readonly DirectorySeparator Default = new DirectorySeparator(Constants.DefaultDirectorySeparator);


        public static DirectorySeparator GetDefaultForPlatform(Platform platform)
        {
            switch(platform)
            {
                case Platform.NonWindows:
                    return DirectorySeparator.DefaultNonWindows;

                case Platform.Windows:
                    return DirectorySeparator.DefaultWindows;

                default:
                    var message = EnumHelper.GetUnexpectedEnumerationValueMessage(platform);
                    throw new ArgumentException(message);
            }
        }

        public static string GetDefaultValueForPlatform(Platform platform)
        {
            var directorySeparator = DirectorySeparator.GetDefaultForPlatform(platform);

            var directorySeparatorValue = directorySeparator.Value;
            return directorySeparatorValue;
        }

        #endregion



        public DirectorySeparator(string value)
            : base(value)
        {
        }
    }
}
