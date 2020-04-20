using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class Rider
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public Status Status { get; set; }

        public float Rating {get;set;}

        public long CityId { get; set; }
        public City City { get; set; }
        public virtual ICollection<RiderRating> RiderRatings { get; set; }
        public virtual ICollection<UserAuthentication> UserAuthentication {get;set;}

    }

}