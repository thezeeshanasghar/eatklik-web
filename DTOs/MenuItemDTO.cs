using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.DTOs
{
    public class MenuItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public ItemSize Size { get; set; }
        public long Price { get; set; }
        public StatusDTO Status { get; set; }
        public long MenuId { get; set; }
        public ICollection<MenuExtraItemDTO> MenuExtraItems { get; set; }

        public enum ItemSize
        {
            Nothing,
            Half,
            Full
        }
    }
}