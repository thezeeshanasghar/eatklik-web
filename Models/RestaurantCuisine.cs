using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class RestaurantCuisine
    {
        public long RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public long CuisineId { get; set; }
        public Cuisine Cuisine { get; set; }
    }

}