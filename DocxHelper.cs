using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;

namespace Avae.Printables
{
    public static class DocxHelper
    {
        public static string ToHtml(string file)
        {
            using var fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            return ToHtml(fs);
        }

        public static string ToHtml(Stream stream)
        {
            var path = Path.Combine(Path.GetTempPath(), "temp.html");            
            using (var docx = WordprocessingDocument.Open(stream, true))
            {
                var html = HtmlConverter.ConvertToHtml(docx, new HtmlConverterSettings());
                File.WriteAllText(path, html.ToStringNewLineOnAttributes());
            }
            return path;
        }
    }
}
