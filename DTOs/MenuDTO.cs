using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.DTOs
{
    public class MenuDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public StatusDTO Status { get; set; }

        public ICollection<MenuItemDTO> MenuItems { get; set; }
    }

}