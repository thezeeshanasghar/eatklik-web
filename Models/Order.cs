using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{

    public class Order
    {
        public long Id { get; set; }
        public long Subtotal { get; set; }
        public long Fee { get; set; }
        public long DeliveryCharges { get; set; }
        public long GST { get; set; }
        public long GrandTotal { get; set; }
        public string Address {get;set;}
        public int PayMethod {get;set;}
        public string Instruction {get;set;}
        public DateTime Created { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public RiderStatus RiderStatus { get; set; }
        public long CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }

        public long CityId { get; set; }
        [JsonIgnore]
        public City City { get; set; }

        public long RestaurantId { get; set; }
        [JsonIgnore]
        public Restaurant Restaurant { get; set; }

        
        public long? RiderId { get; set; }
        [JsonIgnore]
        public virtual Rider Rider { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

}