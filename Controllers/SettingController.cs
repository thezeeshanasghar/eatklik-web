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
    public class SettingController : ControllerBase
    {
        private readonly Context _db;

        public SettingController(Context context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Setting>>> GetAll()
        {
            return await _db.Settings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Setting>> GetSingle(long id)
        {
            var setting = await _db.Settings.FindAsync(id);
            if (setting == null)
                return NotFound();

            return setting;
        }

        [HttpPost]
        public async Task<ActionResult<Setting>> Post(Setting setting)
        {
            _db.Settings.Update(setting);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = setting.Id }, setting);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Setting setting)
        {
            if (id != setting.Id)
                return BadRequest();

            _db.Entry(setting).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var setting = await _db.Settings.FindAsync(id);
            if (setting == null)
                return NotFound();

            _db.Settings.Remove(setting);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}