using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public string UserType { get; set; }
        
    }

}