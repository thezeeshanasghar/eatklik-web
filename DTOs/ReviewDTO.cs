using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.DTOs
{

    public class ReviewDTO
    {
        public long Id { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public StatusDTO Status { get; set; }
        public long CustomerId {get;set;}
        public CustomerDTO Customer{get;set;}
        public long RestaurantId {get;set;}
        public RestaurantDTO Restaurant {get;set;}
    }

}