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
    public class MenuController : ControllerBase
    {
        private readonly Context _db;

        public MenuController(Context context)
        {
            _db = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetSingle(long id)
        {
            var Menu = await _db.Menus.FindAsync(id);
            if (Menu == null)
                return NotFound();

            return Menu;
        }

        [HttpPost]
        public async Task<ActionResult<Menu>> Post(Menu Menu)
        {
            _db.Menus.Update(Menu);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Menu.Id }, Menu);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Menu Menu)
        {
            if (id != Menu.Id)
                return BadRequest();

            _db.Entry(Menu).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var Menu = await _db.Menus.FindAsync(id);

            if (Menu == null)
                return NotFound();

            _db.Menus.Remove(Menu);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/menuitem")]
        public async Task<ActionResult<ICollection<MenuItem>>> GetMenuItems(long id)
        {
            var menu = await _db.Menus.Include(x => x.MenuItems).FirstOrDefaultAsync(x => x.Id == id);
            if (menu == null)
                return NotFound();

            return Ok(menu.MenuItems);
        }
    }
}