using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class UserAuthentication
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public long CustomerId { get; set; } //User May be Customer or Rider
        public int IsVerified { get; set; } // 0 for false , 1 for true
    }

}