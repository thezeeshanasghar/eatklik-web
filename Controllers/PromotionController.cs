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
            //     _db = context;

            //     if (_db.Promotions.Count() == 0)
            //     {
            //         _db.Promotions.Add(new Promotion { Name = "Isb" });
            //         _db.SaveChanges();
            //     }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetAll()
        {
            return await _db.Promotions.ToListAsync();
        }

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

        [HttpPost]
        public async Task<ActionResult<Promotion>> Post(Promotion Name)
        {
            _db.Promotions.Add(Name);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Name.Id }, Name);
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
            var Name = await _db.Promotions.FindAsync(id);

            if (Name == null)
            {
                return NotFound();
            }

            _db.Promotions.Remove(Name);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}