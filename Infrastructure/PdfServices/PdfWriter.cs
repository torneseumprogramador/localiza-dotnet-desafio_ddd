using System;
using System.IO;
using SelectPdf;

namespace Infrastructure.PdfServices
{
    public class PdfWriter : IPdfWriter
    {
        public string Build(string html)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(html);
            var pathPDF = "/RentalPaymentReceipts/RentalPaymentReceipts-{DateTime.Now:ddmmYYYY}.pdf";
            doc.Save($"{AppDomain.CurrentDomain.BaseDirectory}{pathPDF}");
            doc.Close();
            return pathPDF;
        }
    }
}
