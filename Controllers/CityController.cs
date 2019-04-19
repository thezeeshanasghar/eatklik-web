using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly Context _db;

        public CityController(Context context)
        {
            _db = context;

            if (_db.Cities.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _db.Cities.Add(new City { Name = "Isb" });
                _db.SaveChanges();
            }
        }


        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetAll()
        {
            return await _db.Cities.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetSingle(long id)
        {
            var todoItem = await _db.Cities.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<City>> Post(City city)
        {
            _db.Cities.Add(city);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = city.Id }, city);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _db.Entry(city).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var city = await _db.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            _db.Cities.Remove(city);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}