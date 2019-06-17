using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using R5T.NetStandard.IO.Paths.Extensions;


namespace R5T.NetStandard.IO.Paths
{
    public static class DirectoryPathExtensions
    {
        public static void Delete(this DirectoryPath directoryPath, bool recursive = true)
        {
            Directory.Delete(directoryPath.Value, recursive);
        }

        public static IEnumerable<DirectoryPath> EnumerateDirectories(this DirectoryPath directoryPath, SearchPattern searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            foreach (var directoryPathValue in Directory.EnumerateDirectories(directoryPath.Value, searchPattern.Value, searchOption))
            {
                yield return directoryPathValue.AsDirectoryPath();
            }
        }

        public static IEnumerable<DirectoryPath> EnumerateDirectories(this DirectoryPath directoryPath)
        {
            return directoryPath.EnumerateDirectories(SearchPattern.All);
        }

        public static IEnumerable<DirectoryPath> EnumerateDirectories(this DirectoryPath directoryPath, Regex directoryNameRegex, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            foreach (var subDirectoryPath in directoryPath.EnumerateDirectories(SearchPattern.All, searchOption))
            {
                var directoryName = Utilities.GetDirectoryName(subDirectoryPath);
                if (directoryNameRegex.IsMatch(directoryName.Value))
                {
                    yield return subDirectoryPath;
                }
            }
        }

        public static IEnumerable<FilePath> EnumerateFiles(this DirectoryPath directoryPath, SearchPattern searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            foreach (var filePathValue in Directory.EnumerateFiles(directoryPath.Value, searchPattern.Value))
            {
                yield return filePathValue.AsFilePath();
            }
        }

        public static IEnumerable<FilePath> EnumerateFiles(this DirectoryPath directoryPath)
        {
            return directoryPath.EnumerateFiles(SearchPattern.All);
        }

        public static IEnumerable<FilePath> EnumerateFiles(this DirectoryPath directoryPath, Regex fileNameRegex, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            foreach (var filePath in directoryPath.EnumerateFiles(SearchPattern.All, searchOption))
            {
                var fileName = Utilities.GetFileName(filePath);
                if (fileNameRegex.IsMatch(fileName.Value))
                {
                    yield return filePath;
                }
            }
        }

        public static bool Exists(this DirectoryPath directoryPath)
        {
            var exists = Directory.Exists(directoryPath.Value);
            return exists;
        }

        public static DirectoryName GetDirectoryName(this DirectoryPath directoryPath)
        {
            var directoryName = new DirectoryInfo(directoryPath.Value).Name.AsDirectoryName();
            return directoryName;
        }
    }
}
