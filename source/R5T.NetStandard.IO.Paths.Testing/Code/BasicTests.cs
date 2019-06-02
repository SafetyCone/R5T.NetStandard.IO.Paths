using Microsoft.VisualStudio.TestTools.UnitTesting;

using R5T.NetStandard.IO.Paths.Extensions;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.NetStandard.IO.Paths.Testing
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void CombineFileNameWithoutExtensionAndFileExtenion()
        {
            var fileNameWithoutExtension = "temp".AsFileNameWithoutExtension();
            var fileExtension = "txt".AsFileExtension();
            var expected = "temp.txt";

            var fileName = PathUtilities.Combine(fileNameWithoutExtension, fileExtension);

            Assert.AreEqual(expected, fileName.Value);
        }
    }
}
