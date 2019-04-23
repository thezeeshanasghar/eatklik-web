using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class Restaurant
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long MinOrderPrice { get; set; }
        public long MaxOrderPrice { get; set; }
        public string Description { get; set; }

        public string LogoImagePath { get; set; }
        public string CoverImagePath { get; set; }

        public ICollection<RestaurantCuisine> RestaurantCuisines { get; set; }

        public long RestaurantLocationId { get; set; }
        public RestaurantLocation RestaurantLocation { get; set; }

        public ICollection<RestaurantTiming> RestaurantTimings { get; set; }

        public long RestaurantContactId { get; set; }
        public RestaurantContact RestaurantContact { get; set; }

        public ICollection<Review> CustomrReviews {get;set;}

    }

}