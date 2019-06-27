using System;
using System.IO;

using R5T.NetStandard.IO.Paths;
using R5T.NetStandard.IO.Paths.Extensions;
using R5T.NetStandard.OS;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;
using PathUtilities2 = R5T.NetStandard.IO.Paths.UtilitiesExtra;


namespace R5T.NetStandard.IO.Paths.Construction
{
    public static class Examples
    {
        public static void SubMain()
        {
            //Examples.FileNameFromFileNameWithoutExtensionAndExtension();
            //Examples.FileNameWithoutExtensionFromFileNameSegments();
            //Examples.DirectoryNameFromDirectoryNameSegments();
            //Examples.FilePathFromPathSegments();
            //Examples.LinuxStylePathCombine();
            Examples.FilePathRelativeToFilePath();
        }

        /// <summary>
        /// Shows that the relative file path "..\" (parent directory) relative to a file-path is the file-path's directory-path.
        /// Example: C:\R5T.Code.VisualStudio.Types\R5T.Code.VisualStudio.Types.csproj + ..\Temp.txt = C:\R5T.Code.VisualStudio.Types\Temp.txt
        /// </summary>
        public static void FilePathRelativeToFilePath()
        {
            var filePath = @"C:\R5T.Code.VisualStudio.Types\R5T.Code.VisualStudio.Types.csproj".AsFilePath();
            var fileRelativePath = @"..\Temp.txt".AsFileRelativePath();

            var resolvedFilePath = PathUtilities2.GetFilePath(filePath, fileRelativePath);

            var describer = ObjectDescriber.Default;
            var writer = Examples.GetWriter();
            writer.WriteLine($"OSX User Temp Directory Path: {describer.Describe(resolvedFilePath)}"); // Result: C:\R5T.Code.VisualStudio.Types\Temp.txt
        }

        /// <summary>
        /// If you have Linux-style paths, see whether they can be combined.
        /// </summary>
        public static void LinuxStylePathCombine()
        {
            var osxUserRootDirectoryPath = @"/Users/User1".AsDirectoryPath();
            var tempDirectoryName = "temp".AsDirectoryName();

            var osxUserTempDirectoryPath = PathUtilities2.Combine(Platform.NonWindows, osxUserRootDirectoryPath, tempDirectoryName).AsDirectoryPath();

            var describer = ObjectDescriber.Default;
            var writer = Examples.GetWriter();
            writer.WriteLine($"OSX User Temp Directory Path: {describer.Describe(osxUserTempDirectoryPath)}");
        }

        public static void FilePathFromPathSegments()
        {
            var directoryPath = @"C:\Temp".AsDirectoryPath();
            var directoryName = "Images".AsDirectoryName();
            var fileName = "8686.jpg".AsFileName();

            var filePath = PathUtilities2.Combine(Platform.NonWindows, directoryPath, directoryName, fileName);

            var writer = Examples.GetWriter();

            var describer = ObjectDescriber.Default;

            writer.WriteLine($"{describer.Describe(filePath)} = Combine(\n\t{describer.Describe(directoryPath)},\n\t{describer.Describe(directoryName)},\n\t{describer.Describe(fileName)})");
        }

        public static void DirectoryNameFromDirectoryNameSegments()
        {
            var projectNameDirectoryNameSegment = "R5T.NetStandard.IO.Paths".AsDirectoryNameSegment();
            var constructionDirectoryNameSegment = "Construction".AsDirectoryNameSegment();

            var directoryName = PathUtilities.Combine(projectNameDirectoryNameSegment, constructionDirectoryNameSegment);

            var writer = Examples.GetWriter();

            var describer = ObjectDescriber.Default;

            writer.WriteLine($"{describer.Describe(directoryName)} = Combine({describer.Describe(projectNameDirectoryNameSegment)}, {describer.Describe(constructionDirectoryNameSegment)})");
        }

        public static void FileNameWithoutExtensionFromFileNameSegments()
        {
            var projectNameFileNameSegment = "R5T.NetStandard.IO.Paths".AsFileNameSegment();
            var constructionFileNameSegment = "Construction".AsFileNameSegment();

            var fileNameWithoutExtension = PathUtilities.Combine(projectNameFileNameSegment, constructionFileNameSegment);

            var writer = Examples.GetWriter();

            writer.WriteLine($"{fileNameWithoutExtension} = Combine({projectNameFileNameSegment}, {constructionFileNameSegment})");
        }
        
        public static void FileNameFromFileNameWithoutExtensionAndExtension()
        {
            var fileNameWithoutExtension = "temp".AsFileNameWithoutExtension();
            var fileExtension = "txt".AsFileExtension();

            var fileName = PathUtilities.Combine(fileNameWithoutExtension, fileExtension);

            var writer = Examples.GetWriter();

            writer.WriteLine($"{fileName} = Combine({fileNameWithoutExtension}, {fileExtension})");
        }

        public static TextWriter GetWriter()
        {
            var writer = Console.Out;
            return writer;
        }
    }
}
