using HotelClient.Models;
using HotelClient.Services;
using HotelClient.ViewModels;

namespace HotelClient.Views
{
    public partial class BookingPage : ContentPage
    {
        private readonly BookingService bookingService;

        public BookingPage(HotelClient.Models.Hotel selectedHotel, BookingService bookingService)
        {
            InitializeComponent();
            this.bookingService = bookingService;
            BindingContext = new BookingViewModel(selectedHotel, bookingService);
        }

    }
}
