using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{

    public class Coordinates
    {
        public long Id { get; set; }
        public string RiderCoordinates { get; set; }
        public string CustomerCoordinates { get; set; }
        public long OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
    }

}