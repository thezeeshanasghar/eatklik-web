using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.DTOs
{

    public class CustomerDTO
    {
        public long Id { get; set; }
        public string Name {get;set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImagePath {get;set;}
        public string Address {get;set;}
        public StatusDTO Status {get;set;}
        public long CityId {get;set;}
        public CityDTO City {get;set;}
        public ICollection<ReviewDTO> RestaurantReviews {get;set;}
        public ICollection<OrderDTO> CustomerOrders {get;set;}

    }

}