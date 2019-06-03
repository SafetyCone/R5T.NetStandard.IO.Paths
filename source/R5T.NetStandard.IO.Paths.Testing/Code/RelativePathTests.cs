using Microsoft.VisualStudio.TestTools.UnitTesting;

using R5T.NetStandard.IO.Paths.Extensions;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.NetStandard.IO.Paths.Testing
{
    [TestClass]
    public class RelativePathTests
    {
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
