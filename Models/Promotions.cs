using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eatklik.Models
{

    public class Promotion
    {
        public long Id { get; set; }
        public City City { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public PromoType PromoType { get; set; }
        public string URL { get; set; }
        public Status Status { get; set; }
    }

    public enum PromoType
    {
        Image,
        News,
        Video
    }
}