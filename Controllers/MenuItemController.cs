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
    public class MenuItemController : ControllerBase
    {
        private readonly Context _db;

        public MenuItemController(Context context)
        {
            _db = context;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetSingle(long id)
        {
            var MenuItem = await _db.MenuItems.FindAsync(id);
            if (MenuItem == null)
                return NotFound();

            return MenuItem;
        }

        [HttpPost]
        public async Task<ActionResult<MenuItem>> Post(MenuItem MenuItem)
        {
            _db.MenuItems.Update(MenuItem);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = MenuItem.Id }, MenuItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, MenuItem MenuItem)
        {
            if (id != MenuItem.Id)
                return BadRequest();

            _db.Entry(MenuItem).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var MenuItem = await _db.MenuItems.FindAsync(id);

            if (MenuItem == null)
                return NotFound();

            _db.MenuItems.Remove(MenuItem);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}