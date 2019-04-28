using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models.Derived
{

    public class Dashboard
    {
        public long CityCount { get; set; }
        public long CuisineCount { get; set; }
        public long CustomerCount { get; set; }
        public long PromotionCount { get; set; }
        public long RestaurantCount { get; set; }
        public long ReviewCount { get; set; }
        public long RiderCount { get; set; }
    }

}