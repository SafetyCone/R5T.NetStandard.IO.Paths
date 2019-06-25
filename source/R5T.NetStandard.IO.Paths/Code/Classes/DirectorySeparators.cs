using System;

using R5T.NetStandard.OS;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Separates <see cref="PathSegment"/>s (usually directory names and the file name) in a path.
    /// </summary>
    public static class DirectorySeparators
    {
        /// <summary>
        /// Provides the default directory separator on the currently executing platform.
        /// </summary>
        public static readonly string PlatformDefaultDirectorySeparator = Utilities.PlatformDirectorySeparatorValue;

        /// <summary>
        /// Provides the default directory separator on the currently executing platform.
        /// </summary>
        public static readonly DirectorySeparator PlatformDefault = new DirectorySeparator(DirectorySeparators.PlatformDefaultDirectorySeparator);


        public static DirectorySeparator GetDefaultForPlatform(Platform platform)
        {
            switch(platform)
            {
                case Platform.NonWindows:
                    return DirectorySeparator.DefaultNonWindows;

                case Platform.Windows:
                    return DirectorySeparator.DefaultWindows;

                default:
                    var message = EnumHelper.UnexpectedEnumerationValueMessage(platform);
                    throw new ArgumentException(message);
            }
        }

        public static string GetDefaultValueForPlatform(Platform platform)
        {
            var directorySeparator = DirectorySeparators.GetDefaultForPlatform(platform);

            var directorySeparatorValue = directorySeparator.Value;
            return directorySeparatorValue;
        }
    }
}
