using Microsoft.VisualStudio.TestTools.UnitTesting;

using R5T.NetStandard.IO.Paths.Extensions;
using R5T.NetStandard.OS;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.NetStandard.IO.Paths.Testing
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void LinuxStylePathCombine()
        {
            var osxUserRootDirectoryPath = @"/Users/User1".AsDirectoryPath();
            var tempDirectoryName = "temp".AsDirectoryName();
            var expected = @"/Users/User1/temp".AsDirectoryPath();

            var tempDirectoryPath = PathUtilities.Combine(Platform.NonWindows, osxUserRootDirectoryPath, tempDirectoryName).AsDirectoryPath();

            Assert.AreEqual(expected.Value, tempDirectoryPath.Value);
        }

        [TestMethod]
        public void WindowsStylePathCombine()
        {
            var osxUserRootDirectoryPath = @"C:\Users\User1".AsDirectoryPath();
            var tempDirectoryName = "temp".AsDirectoryName();
            var expected = @"C:\Users\User1\temp".AsDirectoryPath();

            var tempDirectoryPath = PathUtilities.Combine(Platform.Windows, osxUserRootDirectoryPath, tempDirectoryName).AsDirectoryPath();

            Assert.AreEqual(expected.Value, tempDirectoryPath.Value);
        }

        [TestMethod]
        public void CreateDropboxDirectoryPath()
        {
            var dropboxWindowsRootDirectoryPath = @"C:\Users\User\Dropbox".AsDirectoryPath();
            var organizationsDirectoryName = "Organizations".AsDirectoryName();
            var organizationDirectoryName = "Rivet".AsDirectoryName();
            var dataDirectoryName = "Data".AsDirectoryName();
            var expected = @"C:\Users\User\Dropbox\Organizations\Rivet\Data";

            var directoryPath = PathUtilities.Combine(dropboxWindowsRootDirectoryPath, organizationsDirectoryName, organizationDirectoryName, dataDirectoryName);

            Assert.AreEqual(expected, directoryPath.Value);
        }

        [TestMethod]
        public void DirectoryNameFromDirectoryNameSegments()
        {
            var projectNameDirectoryNameSegment = "R5T.NetStandard.IO.Paths".AsDirectoryNameSegment();
            var constructionDirectoryNameSegment = "Construction".AsDirectoryNameSegment();
            var expected = "R5T.NetStandard.IO.Paths.Construction";

            var directoryName = PathUtilities.Combine(projectNameDirectoryNameSegment, constructionDirectoryNameSegment);

            Assert.AreEqual(expected, directoryName.Value);
        }

        [TestMethod]
        public void FileNameWithoutExtensionFromFileNameSegments_Combine()
        {
            var projectNameFileNameSegment = "R5T.NetStandard.IO.Paths".AsFileNameSegment();
            var constructionFileNameSegment = "Construction".AsFileNameSegment();
            var expected = "R5T.NetStandard.IO.Paths.Construction";

            var fileNameWithoutExtension = PathUtilities.Combine(projectNameFileNameSegment, constructionFileNameSegment);

            Assert.AreEqual(expected, fileNameWithoutExtension.Value);
        }

        [TestMethod]
        public void FileNameFromFileNameWithoutExtensionAndExtension_Combine()
        {
            var fileNameWithoutExtension = "temp".AsFileNameWithoutExtension();
            var fileExtension = "txt".AsFileExtension();
            var expected = "temp.txt";

            var fileName = PathUtilities.Combine(fileNameWithoutExtension, fileExtension);

            Assert.AreEqual(expected, fileName.Value);
        }
    }
}
