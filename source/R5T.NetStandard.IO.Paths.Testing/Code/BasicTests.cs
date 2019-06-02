using Microsoft.VisualStudio.TestTools.UnitTesting;

using R5T.NetStandard.IO.Paths.Extensions;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.NetStandard.IO.Paths.Testing
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public static void DirectoryNameFromDirectoryNameSegments()
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
