using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{

    public class RestaurantLocation
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DelRadius { get; set; }


        public long RestaurantId { get; set; }

        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
    }

}