using System;
using System.IO;


namespace R5T.NetStandard.IO.Paths
{
    public static class Constants
    {
        public const char DefaultFileExtensionSeparatorChar = '.';
        /// <summary>
        /// Separates the file-name from the file-extenstion.
        /// </summary>
        /// <remarks>
        /// The .NET source code simply hard-codes this (to '.'), but I don't like that.
        /// NOTE: There may be multiple periods in a file name. Only the last token when separated is the file extension.
        /// </remarks>
        public static readonly string DefaultFileExtensionSeparator = Constants.DefaultFileExtensionSeparatorChar.ToString();

        public const char DefaultDirectoryNameSegmentSeparatorChar = '.';
        /// <summary>
        /// Separates directory-name segments.
        /// </summary>
        public static readonly string DefaultDirectoryNameSegmentSeparator = Constants.DefaultDirectoryNameSegmentSeparatorChar.ToString();

        public const char DefaultFileNameSegmentSeparatorChar = '.';
        /// <summary>
        /// Separates file-name segments.
        /// </summary>
        public static readonly string DefaultFileNameSegmentSeparator = Constants.DefaultFileNameSegmentSeparatorChar.ToString();

        public const char DefaultWindowsDirectorySeparatorChar = '\\';
        /// <summary>
        /// Separates directory path segments in Windows-style paths.
        /// </summary>
        public static readonly string DefaultWindowsDirectorySeparator = Constants.DefaultWindowsDirectorySeparatorChar.ToString();

        public const char DefaultNonWindowsDirectorySeparatorChar = '/';
        /// <summary>
        /// Separates directory path segments in non-Windows-style paths.
        /// </summary>
        public static readonly string DefaultNonWindowsDirectorySeparator = Constants.DefaultNonWindowsDirectorySeparatorChar.ToString();

        /// <summary>
        /// Provides the default directory separator on the currently executing platform.
        /// </summary>
        public static readonly string DefaultDirectorySeparator = Utilities.PlatformDirectorySeparator;

        public static readonly char DefaultVolumeSeparatorChar = Path.VolumeSeparatorChar; // ':' as in "C:\..."
        /// <summary>
        /// Separates the volume (drive) from the rest of the path. For example, the ':' in C:\... on Windows.
        /// </summary>
        public static readonly string DefaultVolumeSeparator = Constants.DefaultVolumeSeparatorChar.ToString();

        public static readonly char DefaultPathSeparatorChar = Path.PathSeparator; // ';' as in path1;path2;path3
        /// <summary>
        /// Separates paths in a PATH variable. For example, the ';' in path1;path2;path3.
        /// </summary>
        public static readonly string DefaultPathSeparator = Constants.DefaultPathSeparatorChar.ToString();

        /// <summary>
        /// Name of the current directory in a path.
        /// </summary>
        public static readonly string DefaultCurrentDirectoryName = ".";

        /// <summary>
        /// Name of the parent directory in a path.
        /// </summary>
        public static readonly string DefaultParentDirectoryName = "..";
    }
}
