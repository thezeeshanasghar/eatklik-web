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

        public virtual ICollection<RestaurantCuisine> RestaurantCuisines { get; set; }

        public virtual ICollection<RestaurantLocation> RestaurantLocations { get; set; }

        public virtual ICollection<RestaurantTiming> RestaurantTimings { get; set; }

        public virtual ICollection<RestaurantContact> RestaurantContacts { get; set; }

        public virtual ICollection<Review> CustomrReviews {get;set;}

    }

}