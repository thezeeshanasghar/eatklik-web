using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class Customer
    {
        public long Id { get; set; }
        public string Name {get;set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImagePath {get;set;}
        public string Address {get;set;}
        public Status Status {get;set;}
        public long CityId {get;set;}
        public City City {get;set;}

        public ICollection<Review> RestaurantReviews {get;set;}
    }

}