using System;
using System.IO;


namespace R5T.NetStandard.IO.Paths
{
    public static class DirectoryPathExtensions
    {
        public static bool Exists(this DirectoryPath directoryPath)
        {
            var exists = Directory.Exists(directoryPath.Value);
            return exists;
        }
    }
}
