using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eatklik.Models
{

    public class Setting
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Status Status { get; set; }
    }

}