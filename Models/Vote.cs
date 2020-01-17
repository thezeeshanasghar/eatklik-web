using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class Vote
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string Message { get; set; }

    }

}