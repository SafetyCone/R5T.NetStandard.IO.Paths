using Microsoft.VisualStudio.TestTools.UnitTesting;

using R5T.NetStandard.IO.Paths.Extensions;

using PathUtilities = R5T.NetStandard.IO.Paths.UtilitiesExtra;


namespace R5T.NetStandard.IO.Paths.Testing
{
    [TestClass]
    public class RelativePathTests
    {
        [TestMethod]
        public void FileToFileRelativePath()
        {
            var sourceFilePath = @"C:\R5T.Code.VisualStudio.Types\R5T.Code.VisualStudio.Types.csproj".AsFilePath();
            var destinationFilePath = @"C:\R5T.Code.VisualStudio.Types\Temp.txt".AsFilePath();
            var expectedRelativePath = @"..\Temp.txt".AsFileRelativePath();

            var fileToFileRelativePathPair = new FileToFileRelativePathPair(sourceFilePath, destinationFilePath);

            var relativePath = fileToFileRelativePathPair.GetRelativePath();

            Assert.AreEqual(expectedRelativePath, relativePath);
        }

        [TestMethod]
        public void FilePathRelativeToFilePath()
        {
            var filePath = @"C:\R5T.Code.VisualStudio.Types\R5T.Code.VisualStudio.Types.csproj".AsFilePath();
            var fileRelativePath = @"..\Temp.txt".AsFileRelativePath();
            var expected = @"C:\R5T.Code.VisualStudio.Types\Temp.txt";

            var resolvedFilePath = PathUtilities.GetFilePath(filePath, fileRelativePath);

            Assert.AreEqual(expected, resolvedFilePath.Value);
        }

        [TestMethod]
        public void FilePathFromDirectoryAndRelativeFilePath()
        {
            var directoryPath = @"C:\Temp1\Temp2\Temp3".AsDirectoryPath();
            var relativePath = @"..\..\Temp4\temp5.txt".AsFileRelativePath();
            var expected = @"C:\Temp1\Temp4\temp5.txt";

            var filePath = PathUtilities.Combine(directoryPath, relativePath);

            Assert.AreEqual(expected, filePath.Value);
        }
    }
}
