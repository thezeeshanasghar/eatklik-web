using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{

    public class Restaurant
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long MinOrderPrice { get; set; }
        public long MaxOrderPrice { get; set; }
        public long DelCharges { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string LogoImagePath { get; set; }
        public string CoverImagePath { get; set; }
        public long CityId { get; set; }
        
        [JsonIgnore]
        public City City { get; set; }

        public virtual ICollection<RestaurantCuisine> RestaurantCuisines { get; set; }

        public virtual ICollection<RestaurantLocation> RestaurantLocations { get; set; }

        public virtual ICollection<RestaurantTiming> RestaurantTimings { get; set; }

        public virtual ICollection<RestaurantContact> RestaurantContacts { get; set; }

        public virtual ICollection<RestaurantExtraItem> RestaurantExtraItems { get; set; }

        public virtual ICollection<Menu> RestaurantMenus { get; set; }
        public decimal? Rating {get;set;}
        public int? reviewCount {get;set;}
        public virtual ICollection<Review> CustomrReviews {get;set;}

    }

}