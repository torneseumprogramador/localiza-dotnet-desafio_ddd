using System;
using System.IO;
using SelectPdf;

namespace Infrastructure.PdfServices
{
    public class PdfWriter : IPdfWriter
    {
        public string Build(string html)
        {
            var pathPDF = "/pdf-nao-gerrado-erro-verifique-o-servidor.pdf";
            try
            {
                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertHtmlString(html);
                pathPDF = "/RentalPaymentReceipts/RentalPaymentReceipts-{DateTime.Now:ddmmYYYY}.pdf";
                doc.Save($"{AppDomain.CurrentDomain.BaseDirectory}{pathPDF}");
                doc.Close();
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro.Message);
                Console.WriteLine(erro.StackTrace);
            }

            return pathPDF;
        }
    }
}
