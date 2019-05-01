using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{

    public class RestaurantCuisine
    {
        public long RestaurantId { get; set; }
        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
        public long CuisineId { get; set; }
        [JsonIgnore]
        public Cuisine Cuisine { get; set; }
    }

}