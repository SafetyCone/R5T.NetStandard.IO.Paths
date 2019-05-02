using System;
using System.IO;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Contains utilities for:
    /// * <see cref="System.IO.Path"/> wrappers that add documentation and discussion to the System path utilities.
    /// * Operating on paths as string.
    /// * Operation on strongly-typed paths.
    /// </summary>
    public static class Utilities
    {
        #region Separators.

        /// <summary>
        /// Separates the file-name from the file-extenstion.
        /// </summary>
        /// <remarks>
        /// The .NET source code simply hard-codes this (to '.'), but I don't like that.
        /// NOTE: There may be multiple periods in a file name. Only the last token when separated is the file extension.
        /// </remarks>
        public const char DefaultFileExtensionSeparatorChar = '.';
        public static readonly string DefaultFileExtensionSeparator = Utilities.DefaultFileExtensionSeparatorChar.ToString();
        public const char DefaultFileNameSegmentSeparatorChar = Utilities.DefaultFileExtensionSeparatorChar;
        public static readonly string DefaultFileNameSegmentSeparator = Utilities.DefaultFileNameSegmentSeparatorChar.ToString();
        public const char DefaultWindowsDirectorySeparatorChar = '\\';
        public static readonly string DefaultWindowsDirectorySeparator = Utilities.DefaultWindowsDirectorySeparatorChar.ToString();
        public const char DefaultNonWindowsDirectorySeparatorChar = '/';
        public static readonly string DefaultNonWindowsDirectorySeparator = Utilities.DefaultNonWindowsDirectorySeparatorChar.ToString();
        /// <summary>
        /// Provides the default directory separator on the currently executing platform.
        /// </summary>
        public static readonly string DefaultDirectorySeparator = Utilities.PlatformDirectorySeparator;
        public static readonly char DefaultVolumeSeparatorChar = Path.VolumeSeparatorChar;
        public static readonly string DefaultVolumeSeparator = Utilities.DefaultVolumeSeparatorChar.ToString();
        public static readonly char DefaultPathSeparatorChar = Path.PathSeparator;
        public static readonly string DefaultPathSeparator = Utilities.DefaultPathSeparatorChar.ToString();


        /// <summary>
        /// Get the directory separator used on the current platform (the Windows separator on Windows platforms, the non-Windows separator on non-Windows platforms).
        /// </summary>
        public static string PlatformDirectorySeparator
        {
            get
            {
                var output = OsHelper.PlatformSwitch(
                    () =>
                    {
                        return Utilities.DefaultWindowsDirectorySeparator;
                    },
                    () =>
                    {
                        return Utilities.DefaultNonWindowsDirectorySeparator;
                    },
                    () =>
                    {
                        return Utilities.DefaultNonWindowsDirectorySeparator;
                    });
                return output;
            }
        }
        /// <summary>
        /// Gets the directory separator used on alternate platforms (the non-Windows separator on Windows platforms, the Windows separator on non-Windows platforms).
        /// </summary>
        public static string PlatformDirectorySeparatorAlternate
        {
            get
            {
                var output = Utilities.GetAlternateDirectorySeparator(Utilities.PlatformDirectorySeparator);
                return output;
            }
        }
        /// <summary>
        /// Between the Windows ('\\') and the non-Windows ('/') directory separator, given one, return the other.
        /// </summary>
        public static string GetAlternateDirectorySeparator(string directorySeparator)
        {
            if (directorySeparator == Utilities.DefaultWindowsDirectorySeparator)
            {
                return Utilities.DefaultNonWindowsDirectorySeparator;
            }
            else
            {
                return Utilities.DefaultWindowsDirectorySeparator;
            }
        }
        public static Platform DetectPlatform(string path)
        {
            var containsWindows = path.Contains(Utilities.DefaultWindowsDirectorySeparator);
            if (containsWindows)
            {
                return Platform.Windows;
            }

            var containsNonWindows = path.Contains(Utilities.DefaultNonWindowsDirectorySeparator);
            if (containsNonWindows)
            {
                return Platform.NonWindows;
            }

            throw new Exception($@"Unable to detect platform for path '{path}'.");
        }

        public static string GetDirectorySeparator(string path)
        {
            var platform = Utilities.DetectPlatform(path);
            switch (platform)
            {
                case Platform.NonWindows:
                    return Utilities.DefaultNonWindowsDirectorySeparator;

                case Platform.Windows:
                    return Utilities.DefaultWindowsDirectorySeparator;

                default:
                    throw new Exception(EnumHelper.GetUnexpectedEnumerationValueMessage(platform));
            }
        }

        #endregion

        #region System.IO.Path Wrappers.

        /// <summary>
        /// Returns the <see cref="Path.AltDirectorySeparatorChar"/> value.
        /// Separates directory names in a hierarchical path, the alternative separator for systems following a different convention. This is '/' on Windows.
        /// Example: "/mnt/efs/temp.txt".
        /// </summary>
        public static readonly char AltDirectorySeparatorCharSystem = Path.AltDirectorySeparatorChar;
        /// <summary>
        /// Returns the <see cref="Path.DirectorySeparatorChar"/> value.
        /// Separates directory names in a hierarchical path. This is '\' on Windows.
        /// Example: "C:\temp\temp1\temp2\temp.txt".
        /// </summary>
        public static readonly char DirectorySeparatorCharSystem = Path.DirectorySeparatorChar;
        /// <summary>
        /// Returns the <see cref="Path.PathSeparator"/> value.
        /// Separates separate paths in an environment variable value. Generally ';' on Windows.
        /// Example: "C:\temp1;C:\temp2".
        /// </summary>
        public static readonly char PathSeparatorSystem = Path.PathSeparator;
        /// <summary>
        /// Returns the <see cref="Path.VolumeSeparatorChar"/> value.
        /// Separates the drive (or volume) token from the path. Generally ':' on Windows.
        /// Example: "C:\temp\temp.txt".
        /// </summary>
        public static readonly char VolumeSeparatorCharSystem = Path.VolumeSeparatorChar;
        //public static readonly char[] InvalidPathCharsSystem = Path.InvalidPathChars; // Obsolete.


        /// <summary>
        /// Wraps <see cref="Path.ChangeExtension(string, string)"/>. Changes the extension of the input file path.
        /// Example: ("C:\temp\temp.txt", "jpg") -> "C:\temp\temp.jpg"
        /// The file extension separator can be included, example: ("C:\temp\temp.txt", ".jpeg") -> "C:\temp\temp.jpeg"
        /// </summary>
        public static string ChangeExtensionSystem(string filePath, string extension)
        {
            var output = Path.ChangeExtension(filePath, extension);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.Combine(string[])"/>. Combines path segments into a single path.
        /// Example: (@"C:\", @"temp", @"temp.txt") -> "C:\temp\temp.txt".
        /// The executing platform path separator will be used with no possibility of overloading.
        /// </summary>
        /// <remarks>
        /// For best results, make sure path segments do not start with the platform separator:
        /// Example: (@"C:\", @"\temp", @"temp.txt") -> "\temp\temp.txt".
        /// Example: (@"C:\", @"\temp", @"\temp.txt") -> "\temp.txt".
        /// 
        /// Uses current platform path separator with no overload to specify the path separator. This is a prolematic limitation.
        /// Example: (@"/mnt", @"efs", @"temp.txt") -> "/mnt\efs\temp.txt".
        /// </remarks>
        public static string CombineSystem(params string[] pathSegments)
        {
            var output = Path.Combine(pathSegments);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetDirectoryName(string)"/>. Returns full directory path.
        /// Example: (@"C:\temp\temp.txt") -> "C:\temp".
        /// The returned string will use the executing platform path separator, and if a directory path is given (path ends with a directory separator), the returned path with lack the path separator. 
        /// </summary>
        /// <remarks>
        /// Example: (@"C:\temp\temp") -> "C:\temp".
        /// Example: (@"C:\temp\temp\") -> "C:\temp\temp".
        /// Example: (@"/mnt/efs/temp.txt") -> "\mnt\efs" (on Windows).
        /// Example: (@"/mnt/efs/temp/") -> "\mnt\efs\temp" (on Windows).
        /// </remarks>
        public static string GetDirectoyNameSystem(string path)
        {
            var output = Path.GetDirectoryName(path);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetExtension(string)"/>. Returns the extension of the input file path.
        /// Example: (@"C:\temp\temp.txt") -> ".txt".
        /// Includes the file extension separator char '.', and does not change any capitalization.
        /// </summary>
        public static string GetExtensionSystem(string filePath)
        {
            var output = Path.GetExtension(filePath);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetFileName(string)"/>. Returns the file-name and extension of the input file path.
        /// Example: (@"C:\temp\temp.txt") -> "temp.txt".
        /// This System method works the way it should.
        /// </summary>
        /// <remarks>
        /// Example: (@"/mnt/efs/temp.txt") -> "temp.txt".
        /// </remarks>
        public static string GetFileNameSystem(string filePath)
        {
            var output = Path.GetFileName(filePath);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetFileNameWithoutExtension(string)"/>. Returns only the file-name (without file extension or file extension separator).
        /// Example: (@"C:\temp\temp.txt") -> "temp".
        /// This System method works the way it should.
        /// </summary>
        /// <remarks>
        /// Example: (@"/mnt/efs/temp.txt") -> "temp".
        /// </remarks>
        public static string GetFileNameWithoutExtensionSystem(string filePath)
        {
            var output = Path.GetFileNameWithoutExtension(filePath);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetFullPath(string)"/>. Prefixes the input path with either the current directory, or the current root drive (if the input starts with a path separator).
        /// Example: (@"temp.txt") -> "{Current Directory}\temp.txt".
        /// Example: (@"\temp.txt") -> "C:\temp.txt".
        /// A weird method with schizophrenic behavior based on whether the input begins with a path separator.
        /// </summary>
        /// <remarks>
        /// Example: (@"/temp.txt") -> "C:\temp.txt" (on Windows).
        /// </remarks>
        public static string GetFullPathSystem(string path)
        {
            var output = Path.GetFullPath(path);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetInvalidFileNameChars"/>. Returns the chars that are invalid in file-names on the currently executing platform.
        /// Example: () -> ",&lt;,&gt;,|,:,*,?,\,/ on Windows, plus ~35 that are not printable in the console window.
        /// This method should have overload that allows inputting a platform to get invalid file name characters for that platform.
        /// </summary>
        public static char[] GetInvalidFileNameCharsSystem()
        {
            var output = Path.GetInvalidFileNameChars();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetInvalidPathChars"/>. Returns the chars that are invalid in paths on the currently executing platform.
        /// Example: () -> |, plus ~35 that are not printable in the console window.
        /// This method should have overload that allows inputting a platform to get invalid path characters for that platform.
        /// </summary>
        public static char[] GetInvalidPathCharsSystem()
        {
            var output = Path.GetInvalidPathChars();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetPathRoot(string)"/>. Returns just the root of a path (generally a drive letter).
        /// Example: (@"C:\temp\temp.txt") -> "C:\".
        /// Does not work with non-Windows paths, at least when the currently executing platform is Windows.
        /// </summary>
        /// <remarks>
        /// Example: (@"/mnt/efs/temp.txt") -> "\".
        /// </remarks>
        public static string GetPathRootSystem(string path)
        {
            var output = Path.GetPathRoot(path);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetRandomFileName()"/>. Returns a cryptographically-secure path segment like 'ulcdtig4.v53'
        /// Example () -> "ulcdtig4.v53".
        /// What's with the '.'? The function seems to be trying to produce a file-name with a random file extension. Instead it should produce a random path segment with no file extension separator.
        /// </summary>
        public static string GetRandomFileNameSystem()
        {
            var output = Path.GetRandomFileName();
            return output;
        }

        // Somehow the System.IO.Path in the System.Runtime.Extensions assembly is different than the System.IO.Path in the netstandard assembly?
        // See: https://github.com/dotnet/standard/issues/962
        // Issue is .NET Core 2.1 (class library) has the new methods, while .NET Standard 2.0 does not. Note that .NET Standard 2.1 will have the methods, but it is not yet out!
        //public static string GetRelativePathSystem(string source, string path)
        //{
        //    var output = Path.GetRelativePath(relativeTo, path);
        //    return output;
        //}

        /// <summary>
        /// Wraps <see cref="Path.GetTempFileName()"/>. Returns the path to an actually created temporary file, with a temporary file name, in the %TEMP% directory (../{User}/AppData/Local/Temp).
        /// Example: () -> "C:\Users\david\AppData\Local\Temp\tmpB013.tmp"
        /// This method is a disaster. 1) It should *NOT* create 0 KB file at the given path! 2) it should just return the temp file-name ("tmpB013.tmp"), not the whole path. 3) It should include an -Path() method that takes a directory path to which to add the temp file-name.
        /// </summary>
        public static string GetTempFileNameSystem()
        {
            var output = Path.GetTempFileName();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetTempPath()"/>. Returns the directory-path of the current user's Temp directory.
        /// Example: () -> C:\Users\david\AppData\Local\Temp\
        /// This method should be called "GetTempDirectoryPath", but otherwise works as advertised.
        /// </summary>
        public static string GetTempPathSystem()
        {
            var output = Path.GetTempPath();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.HasExtension(string)"/>.
        /// Litterally just returns whether or not the given path has a file extension (this is useful in determining whether a path should be assumed to be a file-path or a directory-path).
        /// Example: ("C:\temp\temp.txt") -> True.
        /// Example: ("C:\temp\temp") -> False.
        /// This method should have an overload that allows testing whether a path has a specific file-extension.
        /// </summary>
        /// <remarks>
        /// Example: ("C:\temp\temp.") -> False.
        /// </remarks>
        public static bool HasExtensionSystem(string path)
        {
            var output = Path.HasExtension(path);
            return output;
        }

        // Somehow the System.IO.Path in the System.Runtime.Extensions assembly is different than the System.IO.Path in the netstandard assembly?
        ///// <summary>
        ///// Returns whether the path is a Windows path and fully qualified.
        ///// Example: ("C:\temp\temp.txt") -> True.
        ///// This method does not work with non-Windows paths.
        ///// </summary>
        ///// <remarks>
        ///// Example: ("/mnt/efs/temp.txt") -> False (wrongly).
        ///// </remarks>
        //public static void IsPathFullyQualifiedSystem(string path)
        //{
        //    var output = Path.IsPathFullyQualified
        //}

        /// <summary>
        /// Wraps <see cref="Path.IsPathRooted(string)"/>.
        /// Returns whether the path starts with a Windows root drive, or with a non-Windows path separator.
        /// Example: ("C:\temp\temp.txt") -> True.
        /// Example: ("/mnt/efs/temp.txt") -> True.
        /// This method works well.
        /// </summary>
        public static bool IsPathRootedSystem(string path)
        {
            var output = Path.IsPathRooted(path);
            return output;
        }

        // Somehow the System.IO.Path in the System.Runtime.Extensions assembly is different than the System.IO.Path in the netstandard assembly?
        //Join()

        // Somehow the System.IO.Path in the System.Runtime.Extensions assembly is different than the System.IO.Path in the netstandard assembly?
        //TryJoin()

        #endregion

        #region Paths as Strings.

        #endregion

        #region Strongly-Typed Paths.

        #endregion
    }
}
