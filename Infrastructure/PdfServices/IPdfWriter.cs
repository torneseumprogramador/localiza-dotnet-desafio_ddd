namespace Infrastructure.PdfServices
{
    public interface IPdfWriter
    {
        string Build(string path, string body);
    }
}