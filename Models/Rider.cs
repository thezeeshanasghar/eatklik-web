using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class Rider
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public Status Status { get; set; }

        public long CityId { get; set; }
        public City City { get; set; }
    }

}