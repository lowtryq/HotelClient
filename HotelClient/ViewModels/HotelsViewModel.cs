using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HotelClient.Models;

namespace HotelClient.ViewModels
{
    public class HotelsViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        private string _searchName = "";
        private string _searchCity = "";

        public string SearchName
        {
            get => _searchName;
            set { _searchName = value; OnPropertyChanged(); }
        }

        public string SearchCity
        {
            get => _searchCity;
            set { _searchCity = value; OnPropertyChanged(); }
        }

        private double? _minRating;
        private bool? _hasAvailableRooms;

        public double? MinRating
        {
            get => _minRating;
            set { _minRating = value; OnPropertyChanged(); }
        }

        public bool? HasAvailableRooms
        {
            get => _hasAvailableRooms;
            set { _hasAvailableRooms = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Hotel> Hotels { get; } = new();

        public ICommand SearchCommand { get; }

        public HotelsViewModel()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Config.BaseUrl)
            };

            SearchCommand = new Command(async () => await SearchHotels());
        }

        private async Task SearchHotels()
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(SearchName))
                queryParams.Add($"name={Uri.EscapeDataString(SearchName)}");

            if (!string.IsNullOrWhiteSpace(SearchCity))
                queryParams.Add($"city={Uri.EscapeDataString(SearchCity)}");

            if (MinRating.HasValue)
                queryParams.Add($"minRating={MinRating.Value}");

            if (HasAvailableRooms.HasValue)
                queryParams.Add($"hasAvailableRooms={HasAvailableRooms.Value}");

            var url = "api/hotels";

            if (queryParams.Count > 0)
                url += "?" + string.Join("&", queryParams);

            var hotels = await _httpClient.GetFromJsonAsync<List<Hotel>>(url);

            Hotels.Clear();

            if (hotels != null)
            {
                foreach (var hotel in hotels)
                    Hotels.Add(hotel);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}