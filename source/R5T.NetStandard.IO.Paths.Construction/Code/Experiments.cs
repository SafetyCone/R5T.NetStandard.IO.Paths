using System;
using System.IO;


namespace R5T.NetStandard.IO.Paths.Construction
{
    public static class Experiments
    {
        public static void SubMain()
        {
            Experiments.FileURIs();
        }

        /// <summary>
        /// From here: https://stackoverflow.com/questions/1546419/convert-file-path-to-a-file-uri
        /// </summary>
        private static void FileURIs()
        {
            var writer = Console.Out;

            void DisplayUri(Uri uri)
            {
                writer.WriteLine($"{nameof(uri)}.{nameof(uri.ToString)}: {uri.ToString()}");
                writer.WriteLine($"{nameof(uri)}.{nameof(uri.AbsolutePath)}: {uri.AbsolutePath}");
                writer.WriteLine($"{nameof(uri)}.{nameof(uri.AbsoluteUri)}: {uri.AbsoluteUri}");
                writer.WriteLine($"{nameof(uri)}.{nameof(uri.LocalPath)}: {uri.LocalPath}");
            }

            var linuxFileUri = new Uri(new Uri("file://"), "home/foo/README.md");

            DisplayUri(linuxFileUri);
            //uri.ToString: file:///home/foo/README.md
            //uri.AbsolutePath: /home/foo/README.md
            //uri.AbsoluteUri: file:///home/foo/README.md
            //uri.LocalPath: /home/foo/README.md

            var windowsFileUri = new Uri(new Uri("file://"), @"C:\Users\foo\README.md");

            DisplayUri(windowsFileUri);
            //uri.ToString: file:///C:/Users/foo/README.md
            //uri.AbsolutePath: C:/Users/foo/README.md
            //uri.AbsoluteUri: file:///C:/Users/foo/README.md
            //uri.LocalPath: C:\Users\foo\README.md
        }
    }
}
