using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.DTOs
{
    public class MenuExtraItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public string ImagePath { get; set; }
        public long MenuItemId { get; set; }

        [JsonIgnore]
        public virtual MenuItemDTO MenuItem { get; set; }
    }
}