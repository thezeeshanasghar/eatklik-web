using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class Setting
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

}