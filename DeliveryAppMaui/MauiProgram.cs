using Microsoft.Extensions.Logging;
using MailService;
using PdfGenerator;
using PdfGenerator.Models;
using Printer_Service;
using Printer_Service.Models;
using Printer_Service.Data;

namespace DeliveryAppMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            //builder.Services.AddSingleton<WeatherForecastService>();
            // Mail Service Dependency
            builder.Services.AddSingleton<MailContoller>();
            builder.Services.AddSingleton<OutlookController>();

            // Pdf Generator Dependency
            builder.Services.AddSingleton<PdfModel>();
            builder.Services.AddSingleton<PdfController>();

            // Printer Service Dependency
            builder.Services.AddSingleton<Labels>();
            builder.Services.AddSingleton<LabelData>();
            builder.Services.AddSingleton<PrinterController>();


            return builder.Build();
        }
    }
}