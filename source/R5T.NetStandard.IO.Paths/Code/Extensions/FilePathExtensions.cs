using System;
using System.IO;


namespace R5T.NetStandard.IO.Paths
{
    public static class FilePathExtensions
    {
        public static bool Exists(this FilePath filePath)
        {
            var exists = File.Exists(filePath.Value);
            return exists;
        }
    }
}
