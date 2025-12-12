using HotelClient.Services;
using HotelClient.ViewModels;
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

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var selectedHotel = e.CurrentSelection[0] as HotelClient.Models.Hotel;
            if (selectedHotel == null)
                return;

            ((CollectionView)sender).SelectedItem = null;

            await Navigation.PushAsync(new BookingPage(selectedHotel, bookingService));
        }
    }

}

