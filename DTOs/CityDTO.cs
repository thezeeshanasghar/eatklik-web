using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.DTOs
{

    public class CityDTO
    {
       public long Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public StatusDTO Status { get; set; }
    }
}