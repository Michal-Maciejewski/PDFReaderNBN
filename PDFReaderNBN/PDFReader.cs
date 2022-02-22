using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text.RegularExpressions;

namespace PDFReaderNBN
{
    public class PDFReader
    {
        private const string NBNRegex = @"\d[A-z]{3}-\d{2}-\d{2}-[A-z]{3}-\d{3}|\d[A-z]{3}-\d{2}-\d{3}-[A-z]{3}-\d{4}";
        private readonly Regex NBNRegexFinder = new(NBNRegex, RegexOptions.Compiled);
        private List<string> NBNAssests { get; set; } = new List<string>();

        public List<string> GetAssetsFromPDF(FileInfo fileData)
        {
            using (var pdfReader = new PdfReader(fileData))
            {
                GetAssetList(pdfReader);
            }

            return NBNAssests;
        }

        public List<string> GetAssetsFromPDF(MemoryStream fileData)
        {
            using (var pdfReader = new PdfReader(fileData))
            {
                GetAssetList(pdfReader);
            }

            return NBNAssests;
        }

        public List<string> GetAssetsFromPDF(string filePath)
        {
            using (var pdfReader = new PdfReader(filePath))
            {
                GetAssetList(pdfReader);
            }

            return NBNAssests;
        }

        public List<string> GetAssetsFromPDF(MemoryStream fileData, ReaderProperties readerProperties)
        {
            using (var pdfReader = new PdfReader(fileData, readerProperties))
            {
                GetAssetList(pdfReader);
            }

            return NBNAssests;
        }

        public List<string> GetAssetsFromPDF(string filePath, ReaderProperties readerProperties)
        {
            using (var pdfReader = new PdfReader(filePath, readerProperties))
            {
                GetAssetList(pdfReader);
            }

            return NBNAssests;
        }

        private void GetAssetList(PdfReader pdfReader)
        {
            PdfDocument document = new(pdfReader);
            var numberOfPages = document.GetNumberOfPages();
            for (var currentPage = 1; currentPage <= numberOfPages; currentPage++)
            {
                var pdfPage = document.GetPage(currentPage);
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                var currentText = PdfTextExtractor.GetTextFromPage(
                    pdfPage,
                    strategy);

                var listOfAssestInPage = NBNRegexFinder.Matches(currentText);

                NBNAssests.AddRange(listOfAssestInPage.Select(a => a.Value).ToList());
            }

            NBNAssests = NBNAssests.Distinct().ToList();
        }
    }
}
