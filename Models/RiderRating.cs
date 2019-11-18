using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{

    public class RiderRating
    {
        public long Id { get; set; }
        public int Value {get;set;}

        public long CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        
        public long RiderId { get; set; }
        [JsonIgnore]
        public virtual Rider Rider { get; set; }
    }

}