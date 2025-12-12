using HotelClient.Services;
using HotelClient.ViewModels;
using HotelClient.Models;

namespace HotelClient.Views
{
    public partial class MainPage : ContentPage
    {
        public HotelsViewModel ViewModel { get; }
        private readonly BookingService bookingService = new BookingService(new HttpClient());

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new HotelsViewModel();
            BindingContext = ViewModel;
        }

        private async void OnBookButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Hotel selectedHotel)
            {
                await Navigation.PushAsync(new BookingPage(selectedHotel, bookingService));
            }
        }
    }

}

