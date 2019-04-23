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
    public class CuisineController : ControllerBase
    {
        private readonly Context _db;

        public CuisineController(Context context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuisine>>> GetAll()
        {
            return await _db.Cuisines.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cuisine>> GetSingle(long id)
        {
            var cuisine = await _db.Cuisines.FindAsync(id);
            if (cuisine == null)
                return NotFound();

            return cuisine;
        }

        [HttpPost]
        public async Task<ActionResult<Cuisine>> Post(Cuisine Cuisine)
        {
            _db.Cuisines.Update(Cuisine);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Cuisine.Id }, Cuisine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Cuisine Cuisine)
        {
            if (id != Cuisine.Id)
                return BadRequest();

            _db.Entry(Cuisine).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var Cuisine = await _db.Cuisines.FindAsync(id);

            if (Cuisine == null)
                return NotFound();

            _db.Cuisines.Remove(Cuisine);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}