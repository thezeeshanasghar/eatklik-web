using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class RestaurantLocation
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public long latitude { get; set; }
        public long longitude { get; set; }

        public long CityId { get; set; }
        public City City { get; set; }
    }

}