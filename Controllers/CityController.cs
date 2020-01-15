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
    public class CityController : ControllerBase
    {
        private readonly Context _db;

        public CityController(Context context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetAll()
        {
            return await _db.Cities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetSingle(long id)
        {
            var todoItem = await _db.Cities.FindAsync(id);
            if (todoItem == null)
                return NotFound();

            return todoItem;
        }

         [HttpGet("name/{cityName}")]
        public async Task<ActionResult<City>> GetCityId(string cityName)
        {
            var todoItem = await _db.Cities.Where(c=>c.Name.Contains(cityName)).FirstOrDefaultAsync();
            if (todoItem == null)
                return NotFound();

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<City>> Post(City city)
        {
            _db.Cities.Update(city);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = city.Id }, city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, City city)
        {
            if (id != city.Id)
                return BadRequest();

            _db.Entry(city).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var city = await _db.Cities.FindAsync(id);

            if (city == null)
                return NotFound();

            _db.Cities.Remove(city);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}