using System;
using System.IO;
using System.Linq;

using R5T.NetStandard.OS;
using R5T.NetStandard.IO.Paths.Extensions;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Contains utilities for:
    /// * <see cref="Path"/> wrappers that add documentation and discussion to the System path utilities.
    /// * Operating on paths as string.
    /// * Operation on strongly-typed paths.
    /// </summary>
    public static class Utilities
    {
        #region Separators.

        /// <summary>
        /// Get the directory separator used on the current platform (the Windows separator on Windows platforms, the non-Windows separator on non-Windows platforms).
        /// </summary>
        public static string PlatformDirectorySeparator
        {
            get
            {
                string windows() => Constants.DefaultWindowsDirectorySeparator;
                string osx() => Constants.DefaultNonWindowsDirectorySeparator;
                string linux() => Constants.DefaultNonWindowsDirectorySeparator;

                var output = OsHelper.PlatformSwitch(windows, osx, linux);
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

        public static Platform DetectPlatform(string path)
        {
            var containsWindows = path.Contains(Constants.DefaultWindowsDirectorySeparator);
            if (containsWindows)
            {
                return Platform.Windows;
            }

            var containsNonWindows = path.Contains(Constants.DefaultNonWindowsDirectorySeparator);
            if (containsNonWindows)
            {
                return Platform.NonWindows;
            }

            throw new Exception($@"Unable to detect platform for path '{path}'.");
        }

        public static DirectorySeparator GetDirectorySeparator(Platform platform)
        {
            var directorySeparator = DirectorySeparator.GetDefaultForPlatform(platform);
            return directorySeparator;
        }

        public static string GetDirectorySeparatorValue(Platform platform)
        {
            var directorySeparatorValue = DirectorySeparator.GetDefaultValueForPlatform(platform);
            return directorySeparatorValue;
        }

        public static string GetDirectorySeparator(string path)
        {
            var platform = Utilities.DetectPlatform(path);

            var directorySeparator = Utilities.GetDirectorySeparatorValue(platform);
            return directorySeparator;
        }

        /// <summary>
        /// Between the Windows ('\\') and the non-Windows ('/') directory separator, given one, return the other.
        /// If the input directory separator is neither the Windows nor non-Windows separator, the Windows separator is returned.
        /// </summary>
        public static string GetAlternateDirectorySeparator(string directorySeparator)
        {
            if (directorySeparator == Constants.DefaultWindowsDirectorySeparator)
            {
                return Constants.DefaultNonWindowsDirectorySeparator;
            }
            else
            {
                return Constants.DefaultWindowsDirectorySeparator;
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
        /// The method is broken and limited.
        /// Broken in that if any of the path segments startup with the platform directory separator, segments before them are ignored!
        /// Limited in that the executing platform directory separator will be used with no possibility of overloading to allow, for example, creating Linux paths on a Windows machine or Windows paths on a Linux machine.
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

        #region Paths as strings.

        /// <summary>
        /// Given an unresolved path (ex: "C:\Temp1\Temp2\..\Temp3\Temp4.txt"), get a resolved path (ex: "C:\Temp1\Temp3\Temp4.txt").
        /// </summary>
        public static string ResolvePath(string unresolvedPath)
        {
            var unresolvedUri = new Uri(unresolvedPath);
            var localPathUri = unresolvedUri.LocalPath;

            var output = Path.GetFullPath(localPathUri);
            return output;
        }

        /// <summary>
        /// Combines path segments using the specified directory separator, after trimming both platform and platform-alternate directory separators, and replacment of platform-alternate directory separators with platform directory separators.
        /// * All segments except the last are trimmed of ending path segments.
        /// * All segments except the first are trimmed of starting path segments.
        /// * All segments have platform-alternate path separators replaced with platform path separators.
        /// </summary>
        public static string CombineUsingDirectorySeparatorNoResolution(string directorySeparator, params string[] pathSegments)
        {
            var directorySeparatorAlternate = Utilities.GetAlternateDirectorySeparator(directorySeparator);

            var nSegments = pathSegments.Length;

            // Trim both path separators from the ends of all segments except the last.
            for (int iSegment = 0; iSegment < nSegments - 1; iSegment++)
            {
                var pathSegment = pathSegments[iSegment];
                var trimmedPathSegment = pathSegment.TrimEnd(Constants.DefaultWindowsDirectorySeparatorChar, Constants.DefaultNonWindowsDirectorySeparatorChar);
                pathSegments[iSegment] = trimmedPathSegment;
            }

            // Trim both path separators from the starts of all segments after the first.
            for (int iSegment = 1; iSegment < nSegments; iSegment++)
            {
                var pathSegment = pathSegments[iSegment];
                var trimmedPathSegment = pathSegment.TrimStart(Constants.DefaultWindowsDirectorySeparatorChar, Constants.DefaultNonWindowsDirectorySeparatorChar);
                pathSegments[iSegment] = trimmedPathSegment;
            }

            // Replace all platform-alternate path separators with platform path separators.
            for (int iSegment = 0; iSegment < nSegments; iSegment++)
            {
                var pathSegment = pathSegments[iSegment];
                var replacedPathSegment = pathSegment.Replace(directorySeparatorAlternate, directorySeparator);
                pathSegments[iSegment] = replacedPathSegment;
            }

            var output = String.Join(directorySeparator, pathSegments);
            return output;
        }

        /// <summary>
        /// Combines path segments using the specified directory separator, and resolves the combined path.
        /// To get an unresolved path, use <see cref="Utilities.CombineUsingDirectorySeparatorNoResolution(string, string[])"/>.
        /// </summary>
        public static string CombineUsingDirectorySeparator(string directorySeparator, params string[] pathSegments)
        {
            var unresolvedCombinedPathSegment = Utilities.CombineUsingDirectorySeparatorNoResolution(directorySeparator, pathSegments);
            var combinedPathSegment = Utilities.ResolvePath(unresolvedCombinedPathSegment);
            return combinedPathSegment;
        }

        /// <summary>
        /// Combine path segments using the platform directory separator.
        /// </summary>
        public static string Combine(params string[] pathSegments)
        {
            var pathSeparator = Utilities.PlatformDirectorySeparator;

            var output = Utilities.CombineUsingDirectorySeparator(pathSeparator, pathSegments);
            return output;
        }

        /// <summary>
        /// Combine path segments using the Windows directory separator.
        /// </summary>
        public static string CombineWindows(params string[] pathSegments)
        {
            var directorySeparator = Constants.DefaultWindowsDirectorySeparator;

            var output = Utilities.CombineUsingDirectorySeparator(directorySeparator, pathSegments);
            return output;
        }

        /// <summary>
        /// Combine path segments using the non-Windows directory separator.
        /// </summary>
        public static string CombineNonWindows(params string[] pathSegments)
        {
            var directorySeparator = Constants.DefaultNonWindowsDirectorySeparator;

            var output = Utilities.CombineUsingDirectorySeparator(directorySeparator, pathSegments);
            return output;
        }

        public static string GetFileName(string fileNameWithoutExtension, string fileExtensionSeparator, string fileExtension)
        {
            var fileName = $"{fileNameWithoutExtension}{fileExtensionSeparator}{fileExtension}";
            return fileName;
        }        

        #endregion

        #region Strongly-typed paths.

        #region File-name.

        /// <summary>
        /// Combines multiple <see cref="FileNameSegment"/>s into a single <see cref="GenericFileNameSegment"/>.
        /// </summary>
        /// <remarks>
        /// Without knowing the context of use, there is now way to know the type of the combined <see cref="FileNameSegment"/>s.
        /// But, depending on context, the <see cref="GenericFileNameSegment"/> can be converted into the proper file-name type.
        /// </remarks>
        public static GenericFileNameSegment Combine(FileNameSegmentSeparator fileNameSegmentSeparator, params FileNameSegment[] fileNameSegments)
        {
            var fileNameSegmentValue = fileNameSegments.Join(fileNameSegmentSeparator.Value);

            var fileNameSegment = new GenericFileNameSegment(fileNameSegmentValue);
            return fileNameSegment;
        }

        /// <summary>
        /// Combines multiple <see cref="FileNameSegment"/>s into a single <see cref="GenericFileNameSegment"/>.
        /// Uses the <see cref="FileNameSegmentSeparator.Default"/> value.
        /// </summary>
        public static GenericFileNameSegment Combine(params FileNameSegment[] fileNameSegments)
        {
            var fileNameSegment = Utilities.Combine(FileNameSegmentSeparator.Default, fileNameSegments);
            return fileNameSegment;
        }

        /// <summary>
        /// Combines a <see cref="FileNameWithoutExtension"/> with a <see cref="FileExtension"/>, using the specified <see cref="FileExtensionSeparator"/>.
        /// </summary>
        public static FileName Combine(FileNameWithoutExtension fileNameWithoutExtension, FileExtension fileExtension, FileExtensionSeparator fileExtensionSeparator)
        {
            var genericFileNameSegment = Utilities.Combine(fileExtensionSeparator, fileNameWithoutExtension, fileExtension);

            var fileName = genericFileNameSegment.AsFileName();
            return fileName;
        }

        /// <summary>
        /// Combines a <see cref="FileNameWithoutExtension"/> with a <see cref="FileExtension"/>.
        /// Uses the <see cref="FileExtensionSeparator.Default"/> value.
        /// </summary>
        public static FileName Combine(FileNameWithoutExtension fileNameWithoutExtension, FileExtension fileExtension)
        {
            var fileName = Utilities.Combine(fileNameWithoutExtension, fileExtension, FileExtensionSeparator.Default);
            return fileName;
        }

        /// <summary>
        /// Gets the <see cref="FileName"/> composed of the specified <see cref="FileNameWithoutExtension"/> and <see cref="FileExtension"/>, joined using the specified <see cref="FileExtensionSeparator"/>.
        /// </summary>
        public static FileName GetFileName(FileNameWithoutExtension fileNameWithoutExtension, FileExtension fileExtension, FileExtensionSeparator fileExtensionSeparator)
        {
            var fileName = Utilities.Combine(fileNameWithoutExtension, fileExtension, fileExtensionSeparator);
            return fileName;
        }

        /// <summary>
        /// Gets the <see cref="FileName"/> composed of the specified <see cref="FileNameWithoutExtension"/> and <see cref="FileExtension"/>.
        /// Uses the <see cref="FileExtensionSeparator.Default"/> value.
        /// </summary>
        public static FileName GetFileName(FileNameWithoutExtension fileNameWithoutExtension, FileExtension fileExtension)
        {
            var fileName = Utilities.Combine(fileNameWithoutExtension, fileExtension, FileExtensionSeparator.Default);
            return fileName;
        }

        /// <summary>
        /// Combines <see cref="FileNameSegment"/>s into a <see cref="FileNameWithoutExtension"/> using the specified <see cref="FileNameSegmentSeparator"/>.
        /// </summary>
        public static FileNameWithoutExtension GetFileNameWithoutExtension(FileNameSegmentSeparator fileNameSegmentSeparator, params FileNameSegment[] fileNameSegments)
        {
            var genericFileNameSegment = Utilities.Combine(fileNameSegmentSeparator, fileNameSegments);

            var fileNameWithoutExtension = genericFileNameSegment.AsFileNameWithoutExtension();
            return fileNameWithoutExtension;
        }

        /// <summary>
        /// Combines <see cref="FileNameSegment"/>s into a <see cref="FileNameWithoutExtension"/>.
        /// Uses the <see cref="FileNameSegmentSeparator.Default"/> value.
        /// </summary>
        public static FileNameWithoutExtension GetFileNameWithoutExtension(params FileNameSegment[] fileNameSegments)
        {
            var fileNameWithoutExtension = Utilities.GetFileNameWithoutExtension(FileNameSegmentSeparator.Default, fileNameSegments);
            return fileNameWithoutExtension;
        }

        #endregion

        #region Directory-name.

        /// <summary>
        /// Combines multiple <see cref="DirectoryNameSegment"/>s into a single <see cref="GenericDirectoryNameSegment"/>.
        /// Without knowing the context of use, there is now way to know the type of the combined <see cref="DirectoryNameSegment"/>s.
        /// But, depending on context, the <see cref="GenericDirectoryNameSegment"/> can be converted into the proper directory-name type.
        /// </summary>
        public static GenericDirectoryNameSegment Combine(DirectoryNameSegmentSeparator directoryNameSegmentSeparator, params DirectoryNameSegment[] directoryNameSegments)
        {
            var directoryNameSegmentValue = directoryNameSegments.Join(directoryNameSegmentSeparator.Value);

            var directoryNameSegment = new GenericDirectoryNameSegment(directoryNameSegmentValue);
            return directoryNameSegment;
        }

        public static GenericDirectoryNameSegment Combine(params DirectoryNameSegment[] directoryNameSegments)
        {
            var directoryNameSegment = Utilities.Combine(DirectoryNameSegmentSeparator.Default, directoryNameSegments);
            return directoryNameSegment;
        }

        public static DirectoryName GetDirectoryName(DirectoryNameSegmentSeparator directoryNameSegmentSeparator, params DirectoryNameSegment[] directoryNameSegments)
        {
            var genericDirectoryNameSegment = Utilities.Combine(directoryNameSegmentSeparator, directoryNameSegments);

            var directoryName = genericDirectoryNameSegment.AsDirectoryName();
            return directoryName;
        }

        public static DirectoryName GetDirectoryName(params DirectoryNameSegment[] directoryNameSegments)
        {
            var directoryName = Utilities.GetDirectoryName(DirectoryNameSegmentSeparator.Default, directoryNameSegments);
            return directoryName;
        }

        #endregion

        /// <summary>
        /// Combines <see cref="PathSegment"/>s using the specified directory path separator..
        /// An array of <see cref="PathSegment"/>s can only be combined into a <see cref="PathSegment"/>.
        /// Depending on context, a <see cref="PathSegment"/> could be converted to the proper type, for example a <see cref="DirectoryPath"/> or <see cref="FilePath"/>.
        /// </summary>
        public static PathSegment Combine(DirectorySeparator directorySeparator, params PathSegment[] pathSegments)
        {
            var pathSegmentValues = pathSegments.Select(x => x.Value).ToArray();

            var directorySeparatorValue = directorySeparator.Value;

            var combinedValue = Utilities.CombineUsingDirectorySeparator(directorySeparatorValue, pathSegmentValues);

            var pathSegment = combinedValue.AsPathSegment();
            return pathSegment;
        }

        ///// <summary>
        ///// Combines <see cref="PathSegment"/>s using the default directory path separator for the specified <see cref="Platform"/>.
        ///// An array of <see cref="PathSegment"/>s can only be combined into a <see cref="PathSegment"/>.
        ///// Depending on context, a <see cref="PathSegment"/> could be converted to a <see cref="DirectoryPath"/> or <see cref="FilePath"/>.
        ///// </summary>
        //public static PathSegment Combine(Platform platform, params PathSegment[] pathSegments)
        //{
        //    var directorySeparator = DirectorySeparator.GetDefaultForPlatform(platform);

        //    var pathSegment = Utilities.Combine(directorySeparator, pathSegments);
        //    return pathSegment;
        //}

        ///// <summary>
        ///// Combines <see cref="DirectoryPathSegment"/>s using the specified <see cref="DirectorySeparator"/>.
        ///// Produces a <see cref="DirectoryRelativePath"/>, which can be combined with an <see cref="AbsolutePath"/> to get a <see cref="DirectoryPath"/>.
        ///// </summary>
        //public static DirectoryRelativePath Combine(DirectorySeparator directorySeparator, params DirectoryPathSegment[] directoryPathSegments)
        //{
        //    var directoryRelativePath = Utilities.Combine(directorySeparator, directoryPathSegments as PathSegment[]).AsDirectoryRelativePath();
        //    return directoryRelativePath;
        //}

        ///// <summary>
        ///// Combines <see cref="DirectoryPathSegment"/>s using the default <see cref="DirectorySeparator"/> for the specified <see cref="Platform"/>.
        ///// Produces a <see cref="DirectoryRelativePath"/>, which can be combined with an <see cref="AbsolutePath"/> to get a <see cref="DirectoryPath"/>.
        ///// </summary>
        //public static DirectoryRelativePath Combine(Platform platform, params DirectoryPathSegment[] directoryPathSegments)
        //{
        //    var directoryRelativePath = Utilities.Combine(platform, directoryPathSegments as PathSegment[]).AsDirectoryRelativePath();
        //    return directoryRelativePath;
        //}

        #region Directory and File Paths

        public static AbsolutePath Combine(DirectorySeparator directorySeparator, AbsolutePath absolutePath, PathSegment pathSegment)
        {
            var combinedPathValue = Utilities.CombineUsingDirectorySeparator(directorySeparator.Value, absolutePath.Value, pathSegment.Value);

            var combinedPath = combinedPathValue.AsAbsolutePath();
            return combinedPath;
        }

        public static AbsolutePath Combine(Platform platform, AbsolutePath absolutePath, PathSegment pathSegment)
        {
            var directorySeparator = Utilities.GetDirectorySeparator(platform);

            var combinedPath = Utilities.Combine(directorySeparator, absolutePath, pathSegment);
            return combinedPath;
        }

        public static AbsolutePath Combine(AbsolutePath absolutePath, PathSegment pathSegment)
        {
            var directorySeparator = DirectorySeparator.Default;

            var combinedPath = Utilities.Combine(directorySeparator, absolutePath, pathSegment);
            return combinedPath;
        }

        public static AbsolutePath Combine(DirectorySeparator directorySeparator, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var relativePathSegment = Utilities.Combine(directorySeparator, pathSegments);

            var combinedPath = Utilities.Combine(directorySeparator, absolutePath, relativePathSegment);
            return combinedPath;
        }

        public static AbsolutePath Combine(Platform platform, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = Utilities.GetDirectorySeparator(platform);

            var combinedPath = Utilities.Combine(directorySeparator, absolutePath, pathSegments);
            return combinedPath;
        }

        public static AbsolutePath Combine(AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = DirectorySeparator.Default;

            var combinedPath = Utilities.Combine(directorySeparator, absolutePath, pathSegments);
            return combinedPath;
        }

        public static FilePath GetFilePath(DirectorySeparator directorySeparator, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var filePath = Utilities.Combine(directorySeparator, absolutePath, pathSegments).AsFilePath();
            return filePath;
        }

        public static FilePath GetFilePath(Platform platform, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = Utilities.GetDirectorySeparator(platform);

            var filePath = Utilities.GetFilePath(directorySeparator, absolutePath, pathSegments);
            return filePath;
        }

        public static FilePath GetFilePath(AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = DirectorySeparator.Default;

            var filePath = Utilities.GetFilePath(directorySeparator, absolutePath, pathSegments);
            return filePath;
        }

        public static DirectoryPath GetDirectoryPath(DirectorySeparator directorySeparator, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directoryPath = Utilities.Combine(directorySeparator, absolutePath, pathSegments).AsDirectoryPath();
            return directoryPath;
        }

        public static DirectoryPath GetDirectoryPath(Platform platform, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = Utilities.GetDirectorySeparator(platform);

            var directoryPath = Utilities.GetDirectoryPath(directorySeparator, absolutePath, pathSegments);
            return directoryPath;
        }

        public static DirectoryPath GetDirectoryPath(AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directorySeparator = DirectorySeparator.Default;

            var directoryPath = Utilities.GetDirectoryPath(directorySeparator, absolutePath, pathSegments);
            return directoryPath;
        }

        public static AbsolutePath ResolvePath(AbsolutePath unresolvedAbsolutePath)
        {
            var resolvedPath = Utilities.ResolvePath(unresolvedAbsolutePath.Value).AsAbsolutePath();
            return resolvedPath;
        }

        public static DirectoryPath ResolvePath(DirectoryPath unresolvedDirectoryPath)
        {
            var resolvedDirectoryPath = Utilities.ResolvePath(unresolvedDirectoryPath.Value).AsDirectoryPath();
            return resolvedDirectoryPath;
        }

        public static FilePath ResolvePath(FilePath unresolvedFilePath)
        {
            var resolvedFilePath = Utilities.ResolvePath(unresolvedFilePath.Value).AsFilePath();
            return resolvedFilePath;
        }

        #endregion

        #endregion
    }
}
