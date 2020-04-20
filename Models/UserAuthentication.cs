using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{

    public class UserAuthentication
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        // public long CustomerId { get; set; } 
        public long PersonId { get; set; } 
   
        // public virtual Rider Rider { get; set; } 
       
        // public virtual Customer Customer { get; set; } 
        public int IsVerified { get; set; } // 0 for false , 1 for true
    }

}