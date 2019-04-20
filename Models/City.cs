using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eatklik.Models
{
    public class City
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get;set;}
        public Status Status { get; set;}
    }

}