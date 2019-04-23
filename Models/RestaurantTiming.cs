using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class RestaurantTiming
    {
        public long Id { get; set; }
        public WeekDay WeekDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public enum WeekDay
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

}