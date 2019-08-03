using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.DTOs
{

    public class RestaurantDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long MinOrderPrice { get; set; }
        public long MaxOrderPrice { get; set; }
        public string Description { get; set; }

        public string LogoImagePath { get; set; }
        public string CoverImagePath { get; set; }

        public  ICollection<CuisineDTO> cuisines { get; set; }
        public  ICollection<MenuDTO> RestaurantMenus { get; set; }

    }

}