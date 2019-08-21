using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{
    public class OrderItem
    {
        public long Id { get; set; }
        public string Name {get;set;}
        public string Size {get;set;}
        public long Price {get;set;}
        public long Quantity {get;set;}
        public long Total {get;set;}
        public virtual Order Order {get;set;} 
    }

}