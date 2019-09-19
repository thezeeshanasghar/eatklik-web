using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eatklik.DTOs;
using eatklik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MenuExtraItemController : ControllerBase
    {
        private readonly Context _db;
        private readonly IMapper _mapper;

        public MenuExtraItemController(Context context, IMapper mapper)
        {
            _db = context;
            this._mapper = mapper;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuExtraItem>> GetSingle(long id)
        {
            var MenuExtraItem = await _db.MenuExtraItems.FindAsync(id);
            if (MenuExtraItem == null)
                return NotFound();

            return MenuExtraItem;
        }

        [HttpPost]
        public async Task<Response<MenuExtraItemDTO>> Post(MenuExtraItemDTO menuExtraItemDTO)
        {
            try
            {
                MenuExtraItem menuExtraItem = _mapper.Map<MenuExtraItem>(menuExtraItemDTO);
                _db.MenuExtraItems.Update(menuExtraItem);
                await _db.SaveChangesAsync();
                menuExtraItemDTO.Id = menuExtraItem.Id;
                return new Response<MenuExtraItemDTO>(true, null, menuExtraItemDTO);
            }
            catch (Exception ex)
            {
                return new Response<MenuExtraItemDTO>(false, ex.Message, null);
            }

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var MenuExtraItem = await _db.MenuExtraItems.FindAsync(id);

            if (MenuExtraItem == null)
                return NotFound();

            _db.MenuExtraItems.Remove(MenuExtraItem);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        //get all extra items of an item
        [HttpGet("{itemId}/extraItem")]
        public async Task<Response<List<MenuExtraItemDTO>>> GetMenuExtraItems(long itemId)
        {
            try
            {
                var MenuItem = await _db.MenuItems.Include(x => x.MenuExtraItems).FirstOrDefaultAsync(x => x.Id == itemId);
                List<MenuExtraItemDTO> menuExtraItemDTOs = _mapper.Map<List<MenuExtraItemDTO>>(MenuItem.MenuExtraItems);
                if (menuExtraItemDTOs.Count == 0)
                    return new Response<List<MenuExtraItemDTO>>(false, "Not Found", null);
                return new Response<List<MenuExtraItemDTO>>(true, null, menuExtraItemDTOs);
            }
            catch (Exception ex)
            {
                return new Response<List<MenuExtraItemDTO>>(false, ex.Message, null);
            }
        }
    }
}