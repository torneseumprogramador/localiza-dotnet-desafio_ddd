namespace Infrastructure.PdfServices
{
    public interface IPdfWriter
    {
        string Build(string body);
    }
}