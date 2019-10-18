using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{
    public class RestaurantExtraItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Size {get;set;}
        public long Price { get; set; }
        public string ImagePath { get; set; }
        
        public long RestaurantId { get; set; }
        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
    }

}