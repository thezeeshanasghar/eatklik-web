using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class Order
    {
        public long Id { get; set; }
        public long Subtotal { get; set; }
        public long Fee { get; set; }
        public long GST { get; set; }
        public long GrandTotal { get; set; }
        public Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

}