namespace TodoApi.Models
{
    public class Rider
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long CityId { get; set; }

        public City City { get; set; }
        // public string ImagePath { get;set;}
    }
}