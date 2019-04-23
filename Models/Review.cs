using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class Review
    {
        public long Id { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public Status Status { get; set; }


        public long CustomerId {get;set;}
        public Customer Customer{get;set;}
        public long RestaurantId {get;set;}
        public Restaurant Restaurant {get;set;}
    }

}