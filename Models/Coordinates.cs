using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class Coordinates
    {
        public long Id { get; set; }
        public string RiderCoordinates { get; set; }
        public string CustomerCoordinates { get; set; }
        public long OrderId { get; set; }
    }

}