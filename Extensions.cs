using Avalonia;
using OpenXmlPowerTools;
using SkiaSharp;

namespace Avae.Printables
{
    public static class Extensions
    {
        public static AppBuilder UseDocxPrintables(this AppBuilder builder)
        {
            var implementation = Printable.Default;
            if(implementation is null)
                throw new ArgumentNullException(nameof(implementation), "UsePrintables must be set before.");

#if WINDOWS10_0_19041_0_OR_GREATER || MACOS || GTK || IOS || BROWSER || ANDROID

            if (implementation is PrintingService service)
            {
#if WINDOWS10_0_19041_0_OR_GREATER

                service.Conversions.Add(".docx", (file) => HtmlHelper.ConvertToPdf(DocxHelper.ToHtml(file)));
                if (Printable.RENDERING == RENDERING.PDF)
                {
                    service.Entries.Add(".docx", async (title, file) => new PdfPrinter(PrintingService.GetActiveWindow(), title, await HtmlHelper.ConvertToPdf(DocxHelper.ToHtml(file))));
                }
                else
                { 
                    service.Entries.Add(".docx", async (title, file) =>
                    {
                        var printer = new HtmlPrinter(DocxHelper.ToHtml(file));
                        await printer.ShowPrintUI();
                        return null!;
                    });
                }
#elif MACOS      
                service.Entries.Add(".docx", (title, file) => PrintingService.PrintHtml(title, DocxHelper.ToHtml(file)));
#elif GTK    
                service.Entries.Add(".docx", (title, file) => PrintingService.PrintHtml(title, DocxHelper.ToHtml(file)));
#elif IOS    
                service.Entries.Add(".docx", (title, file) => PrintingService.PrintHtml(title, DocxHelper.ToHtml(file)));
#elif BROWSER        
                service.Entries.Add(".docx", async (file, stream) =>
                {
                    if (stream is not null)
                    {
                        using var readWriteStream = new MemoryStream();
                        await stream.CopyToAsync(readWriteStream);
                        readWriteStream.Position = 0; // rewind for use
                        var html = DocxHelper.ToHtml(readWriteStream);
                        await service.PrintAsync(html, null);
                    }
                    return new PrintingService.Response() { Base64 = string.Empty, Stop = true };
                });
#elif ANDROID        
                service.Entries.Add(".docx", (title, file) => PrintingService.PrintHtml(title, DocxHelper.ToHtml(file)));
#endif
                }

#endif
            return builder;
        }
    }
}
