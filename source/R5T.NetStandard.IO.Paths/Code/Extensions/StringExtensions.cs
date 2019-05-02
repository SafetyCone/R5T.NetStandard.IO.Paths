using System;


namespace R5T.NetStandard.IO.Paths.Extensions
{
    public static class StringExtensions
    {
        public static DirectoryName ToDirectoryName(this string value)
        {
            var directoryName = new DirectoryName(value);
            return directoryName;
        }

        public static DirectoryPath ToDirectoryPath(this string value)
        {
            var directoryPath = new DirectoryPath(value);
            return directoryPath;
        }

        public static DirectoryRelativePath ToDirectoryRelativePath(this string value)
        {
            var directoryRelativePath = new DirectoryRelativePath(value);
            return directoryRelativePath;
        }

        public static FileExtension ToFileExtension(this string value)
        {
            var fileExtension = new FileExtension(value);
            return fileExtension;
        }

        public static FileName ToFileName(this string value)
        {
            var fileName = new FileName(value);
            return fileName;
        }

        public static FileNameWithoutExtension ToFileNameWithoutExtension(this string value)
        {
            var fileNameWithoutExtension = new FileNameWithoutExtension(value);
            return fileNameWithoutExtension;
        }

        public static FilePath ToFilePath(this string value)
        {
            var filePath = new FilePath(value);
            return filePath;
        }

        public static FileRelativePath ToFileRelativePath(this string value)
        {
            var fileRelativePath = new FileRelativePath(value);
            return fileRelativePath;
        }
    }
}
