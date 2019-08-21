using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.DTOs
{

    public class OrderItemDTO
    {
       public long Id { get; set; }
        public string Name {get;set;}
        public string Size {get;set;}
        public long Price {get;set;}
        public long Quantity {get;set;}
        public long Total {get;set;}
        public OrderDTO Order {get;set;} 
    }

}