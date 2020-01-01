using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace eatklik.Models
{

    public class Review
    {
        public long Id { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }


        public long CustomerId {get;set;}       
        public virtual Customer Customer{get;set;}
        public long RestaurantId {get;set;}
        [JsonIgnore]
        public virtual Restaurant Restaurant {get;set;}
    }

}