using System;
using System.IO;
using IronPdf;

namespace Infrastructure.PdfServices
{
    public class PdfWriter : IPdfWriter
    {
        public string Build(string path, string html)
        {
            var pathPDF = "/pdf-nao-gerrado-erro-verifique-o-servidor.pdf";
            try
            {
                //TODO o ideal é enviar para um bucket, porém como é somente um teste ficará no local por enquanto
                pathPDF = $"/RentalPaymentReceipts/RentalPaymentReceipts-{DateTime.Now:dd-mm-yyyy}.pdf";
                var renderer = new HtmlToPdf();
                renderer.RenderHtmlAsPdf(html).SaveAs($"{path}/wwwroot{pathPDF}");
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
