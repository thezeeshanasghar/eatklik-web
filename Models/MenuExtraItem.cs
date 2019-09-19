
using Newtonsoft.Json;

namespace eatklik.Models
{
    public class MenuExtraItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public string ImagePath { get; set; }
        public long MenuItemId { get; set; }

        [JsonIgnore]
        public virtual MenuItem MenuItem { get; set; }

    }
}