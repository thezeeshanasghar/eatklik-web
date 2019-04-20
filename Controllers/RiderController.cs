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
    public class RiderController : ControllerBase
    {
        private readonly Context _db;

        public RiderController(Context context)
        {
            _db = context;

            if (_db.Riders.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _db.Riders.Add(new Rider { Name = "riders" });
                _db.SaveChanges();
            }
        }


        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rider>>> GetAll()
        {
            return await _db.Riders.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rider>> GetSingle(long id)
        {
            var todoItem = await _db.Riders.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<Rider>> Post(Rider city)
        {
            _db.Riders.Add(city);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = city.Id }, city);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Rider Name)
        {
            if (id != Name.Id)
            {
                return BadRequest();
            }

            _db.Entry(Name).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var name = await _db.Riders.FindAsync(id);

            if (name == null)
            {
                return NotFound();
            }

            _db.Riders.Remove(name);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}