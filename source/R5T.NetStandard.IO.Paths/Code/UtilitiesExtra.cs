using System;
using System.IO;

using R5T.NetStandard.OS;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Contains utilities for:
    /// * <see cref="Path"/> wrappers that add documentation and discussion to the System path utilities.
    /// * Operating on paths as string.
    /// * Operation on strongly-typed paths.
    /// </summary>
    public static class UtilitiesExtra
    {
        #region Separators

        /// <summary>
        /// Get the directory separator used on the current platform (the Windows separator on Windows platforms, the non-Windows separator on non-Windows platforms).
        /// </summary>
        public static string PlatformDirectorySeparatorValue
        {
            get
            {
                string windows() => Constants.WindowsDirectorySeparator;
                string osx() => Constants.NonWindowsDirectorySeparator;
                string linux() => Constants.NonWindowsDirectorySeparator;

                var output = OSHelper.OSPlatformSwitch(windows, osx, linux);
                return output;
            }
        }
        /// <summary>
        /// Gets the directory separator used on alternate platforms (the non-Windows separator on Windows platforms, the Windows separator on non-Windows platforms).
        /// </summary>
        public static string PlatformDirectorySeparatorValueAlternate
        {
            get
            {
                var output = Utilities.GetAlternateDirectorySeparator(UtilitiesExtra.PlatformDirectorySeparatorValue);
                return output;
            }
        }

        public static Platform DetectPlatform(string path)
        {
            var containsWindows = path.Contains(Constants.WindowsDirectorySeparator);
            if (containsWindows)
            {
                return Platform.Windows;
            }

            var containsNonWindows = path.Contains(Constants.NonWindowsDirectorySeparator);
            if (containsNonWindows)
            {
                return Platform.NonWindows;
            }

            throw new Exception($@"Unable to detect platform for path '{path}'.");
        }

        public static DirectorySeparator GetDirectorySeparator(Platform platform)
        {
            var directorySeparator = DirectorySeparators.GetDefaultForPlatform(platform);
            return directorySeparator;
        }

        public static DirectorySeparator GetAlternateDirectorySeparator(Platform platform)
        {
            var alternatePlatform = platform.AlternatePlatform();

            var alternateDirectorySeparator = UtilitiesExtra.GetDirectorySeparator(alternatePlatform);
            return alternateDirectorySeparator;
        }

        public static string GetDirectorySeparatorValue(Platform platform)
        {
            var directorySeparatorValue = DirectorySeparators.GetDefaultValueForPlatform(platform);
            return directorySeparatorValue;
        }

        #endregion

        #region Paths as strings

        #region Separators

        /// <summary>
        /// Ensures that the output path uses the specified path separator.
        /// </summary>
        public static string EnsureDirectorySeparator(string path, Platform platform)
        {
            var directorySeparatorValue = UtilitiesExtra.GetDirectorySeparatorValue(platform);

            var output = Utilities.EnsureDirectorySeparator(path, directorySeparatorValue);
            return output;
        }

        /// <summary>
        /// Ensures that the output path uses the current platform directory separator.
        /// </summary>
        public static string EnsureDirectorySeparator(string path)
        {
            var directorySeparatorValue = UtilitiesExtra.PlatformDirectorySeparatorValue;

            var output = Utilities.EnsureDirectorySeparator(path, directorySeparatorValue);
            return output;
        }

        #endregion

        /// <summary>
        /// Combine path segments using the platform directory separator.
        /// </summary>
        public static string Combine(params string[] pathSegments)
        {
            var pathSeparator = UtilitiesExtra.PlatformDirectorySeparatorValue;

            var output = Utilities.CombineUsingDirectorySeparator(pathSeparator, pathSegments);
            return output;
        }

        #endregion

        #region Strongly-typed paths.

        public static FilePath RelativeToCurrentDirectory(FileName fileName)
        {
            var filePath = UtilitiesExtra.Combine(Utilities.CurrentDirectoryPath, fileName).AsFilePath();
            return filePath;
        }

        #region Directory and File Paths

        public static AbsolutePath Combine(Platform platform, AbsolutePath absolutePath, PathSegment pathSegment)
        {
            var directorySeparator = UtilitiesExtra.GetDirectorySeparator(platform);

            var combinedPath = Utilities.Combine(directorySeparator, absolutePath, pathSegment);
            return combinedPath;
        }

        public static AbsolutePath Combine(AbsolutePath absolutePath, PathSegment pathSegment)
        {
            var directorySeparator = DirectorySeparators.PlatformDefault;

            var combinedPath = Utilities.Combine(directorySeparator, absolutePath, pathSegment);
            return combinedPath;
        }

        public static AbsolutePath Combine(Platform platform, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = UtilitiesExtra.GetDirectorySeparator(platform);

            var combinedPath = Utilities.Combine(directorySeparator, absolutePath, pathSegments);
            return combinedPath;
        }

        public static AbsolutePath Combine(AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = DirectorySeparators.PlatformDefault;

            var combinedPath = Utilities.Combine(directorySeparator, absolutePath, pathSegments);
            return combinedPath;
        }

        public static FilePath GetFilePath(Platform platform, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = UtilitiesExtra.GetDirectorySeparator(platform);

            var filePath = Utilities.GetFilePath(directorySeparator, absolutePath, pathSegments);
            return filePath;
        }

        public static FilePath GetFilePath(AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = DirectorySeparators.PlatformDefault;

            var filePath = Utilities.GetFilePath(directorySeparator, absolutePath, pathSegments);
            return filePath;
        }

        public static DirectoryPath GetDirectoryPath(Platform platform, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = UtilitiesExtra.GetDirectorySeparator(platform);

            var directoryPath = Utilities.GetDirectoryPath(directorySeparator, absolutePath, pathSegments);
            return directoryPath;
        }

        public static DirectoryPath GetDirectoryPath(AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = DirectorySeparators.PlatformDefault;

            var directoryPath = Utilities.GetDirectoryPath(directorySeparator, absolutePath, pathSegments);
            return directoryPath;
        }

        #endregion

        #endregion
    }
}
