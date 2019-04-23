using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{
    public class RestaurantContact
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string URL { get; set; }
    }

}