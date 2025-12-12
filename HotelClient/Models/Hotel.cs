using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClient.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public double Rating { get; set; }
        public bool HasAvailableRooms { get; set; }
    }
}
