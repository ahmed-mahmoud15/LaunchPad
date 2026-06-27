using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using CloudinaryDotNet.Actions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;

namespace Infrastructure.Services
{
    public class PdfParser : IPdfParser
    {
        public Task<string> ExtractTextAsync(Stream pdfStream)
        {
            using var memoryStream = new MemoryStream();
            pdfStream.CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();

            using var document = PdfDocument.Open(bytes);

            var sb = new StringBuilder();

            foreach(var page in document.GetPages())
            {
                var words = NearestNeighbourWordExtractor.Instance.GetWords(page.Letters);

                foreach (var word in words)
                {
                    sb.Append(word.Text);
                    sb.Append(' ');
                }

                sb.AppendLine();
            }

            return Task.FromResult(sb.ToString()); 
        }
    }
}
