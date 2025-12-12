using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using HotelClient.Models;

namespace HotelClient.Services
{
    public class BookingService
    {
        private readonly HttpClient httpClient;

        public BookingService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<bool> CreateBookingAsync(BookingRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://localhost:7297/api/Hotels", request);
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
