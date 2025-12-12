using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClient.Models
{
    public class BookingRequest
    {
        public string HotelId { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public System.DateTime CheckInDate { get; set; }
        public System.DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
