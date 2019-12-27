using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{
    public class Menu
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public Status Status { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
         public ICollection<MenuExtraItem> MenuExtraItems { get; set; }
        public long RestaurantId { get; set; }
        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
    }

}