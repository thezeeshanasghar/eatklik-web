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
        public DateTime Created { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        public long CustomerId { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

}