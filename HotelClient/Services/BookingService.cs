using HotelClient.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HotelClient.Services
{
    public class BookingService
    {
        private readonly HttpClient httpClient;

        public BookingService()
        {
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri(Config.BaseUrl)
            };
        }

        public async Task<bool> CreateBookingAsync(BookingRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/bookings", request);
                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Ошибка сервера: {response.StatusCode}, тело: {content}");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при отправке бронирования: {ex.Message}");
                return false;
            }
        }
    }
}