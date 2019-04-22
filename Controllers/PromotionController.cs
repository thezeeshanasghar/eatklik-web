using eatklik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly Context _db;

        public PromotionController(Context context)
        {
            _db = context;

            // if (_db.Riders.Count() == 0)
            // {
            //     // Create a new TodoItem if collection is empty,
            //     // which means you can't delete all TodoItems.
            //     _db.Riders.Add(new Rider { Name = "riders", CityId = 1 });
            //     _db.SaveChanges();
            // }
        }


        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetAll()
        {
            return await _db.Promotions.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Promotion>> GetSingle(long id)
        {
            var todoItem = await _db.Promotions.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<Promotion>> Post(Promotion name)
        {
            _db.Promotions.Add(name);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = name.Id }, name);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Promotion Name)
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
            var name = await _db.Promotions.FindAsync(id);

            if (name == null)
            {
                return NotFound();
            }

            _db.Promotions.Remove(name);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/city")]
        public async Task<ActionResult<City>> GetPromotionCity(long id)
        {
            var promotion = await _db.Promotions.Include(x => x.City).FirstOrDefaultAsync(x => x.Id == id);
            // _db.Entry(rider)
            //     .Reference(b => b.City)
            //     .Load();

            if (promotion == null)
            {
                return NotFound();
            }

            return promotion.City;
        }
    }
}