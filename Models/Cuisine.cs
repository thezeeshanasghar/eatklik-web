using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{

    public class Cuisine
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        
        [JsonIgnore]
        public ICollection<RestaurantCuisine> RestaurantCuisines { get; set; }
    }

}