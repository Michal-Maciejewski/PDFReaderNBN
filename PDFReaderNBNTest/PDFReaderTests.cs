using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PDFReaderNBN;

namespace PDFReadNBNTest
{
    public class PDFReaderTests
    {
        private string FilePath { get; set; } = "";
        private readonly PDFReader PDFReader = new();

        [SetUp]
        public void Setup()
        {
            IConfiguration configuration;
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<PDFReaderTests>();

            configuration = builder.Build();

            FilePath = configuration["FilePath"];
        }

        [TestCase("Test1.pdf")]
        public void PDFReader_GetAssetsFromPDF_FilePath_ReturnsValidList(string fileName)
        {
            var filePath = FilePath + fileName;
            var expectedCount = 57;

            var list = PDFReader.GetAssetsFromPDF(filePath);
            Assert.NotNull(list);

            var actualCount = list.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}