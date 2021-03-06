using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{

    public class Customer
    {
        public long Id { get; set; }
        public string Name {get;set;}
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImagePath {get;set;}
        public string Address {get;set;}
        public Status Status {get;set;}
        public int Code { get; set; }
        public int IsVerified { get; set; }
        // public long CityId {get;set;}
        // [JsonIgnore]
        // public City City {get;set;}

      //  public virtual ICollection<Review> RestaurantReviews {get;set;}
        public virtual ICollection<Order> CustomerOrders {get;set;}
      // public virtual ICollection<UserAuthentication> UserAuthentication {get;set;}
    }

}