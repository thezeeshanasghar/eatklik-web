using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public async Task<ActionResult<MenuExtraItem>> Post(MenuExtraItem menuExtraItemDTO)
        {
            _db.MenuExtraItems.Update(menuExtraItemDTO);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSingle), new { id = menuExtraItemDTO.Id }, menuExtraItemDTO);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
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
        public async Task<ActionResult<ICollection<MenuExtraItem>>> GetMenuExtraItems(long itemId)
        {
            var items = await _db.MenuItems.Include(x => x.MenuExtraItems).FirstOrDefaultAsync(x => x.Id == itemId);
            if (items == null)
                return NotFound();
            return Ok(items);

        }
    }
}