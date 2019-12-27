using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Models
{
    public class MenuExtraItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        // public ItemSize Size { get; set; }
        public string Size {get; set;}
        public long Price { get; set; }
        public Status Status { get; set; }

        public long MenuId { get; set; }

        [JsonIgnore]
        public Menu Menu { get; set; }
    }

    // public enum ItemSize
    // {
    //     Nothing,
    //     Half,
    //     Full
    // }
}