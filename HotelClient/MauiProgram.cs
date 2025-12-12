using HotelClient.Services;
using Microsoft.Extensions.Logging;

namespace HotelClient
{
    public class SomeViewModel
    {
        private readonly BookingService _bookingService;

        public SomeViewModel(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
    }
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<BookingService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}