using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public MenuExtraItemController(Context context)
        {
            _db = context;
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
        public async Task<ActionResult<MenuExtraItem>> Post(MenuExtraItem MenuExtraItem)
        {
            _db.MenuExtraItems.Update(MenuExtraItem);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = MenuExtraItem.Id }, MenuExtraItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, MenuExtraItem MenuExtraItem)
        {
            if (id != MenuExtraItem.Id)
                return BadRequest();

            _db.Entry(MenuExtraItem).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
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
    
    }
}