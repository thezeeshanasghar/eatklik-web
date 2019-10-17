using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{

    public class RestaurantLocation
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public long RestaurantId { get; set; }

        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
    }

}