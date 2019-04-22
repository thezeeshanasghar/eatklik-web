using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eatklik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase {
        private readonly Context _db;

        public PromotionController (Context context) {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetAll () {
            return await _db.Promotions.ToListAsync ();
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Promotion>> GetSingle (long id) {
            var todoItem = await _db.Promotions.FindAsync (id);

            if (todoItem == null) {
                return NotFound ();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<Promotion>> Post (Promotion promotion) {

            _db.Promotions.Add (promotion);
            await _db.SaveChangesAsync ();

            return CreatedAtAction (nameof (GetSingle), new { id = promotion.Id }, promotion);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Put (long id, Promotion promotion) {
            if (id != promotion.Id) {
                return BadRequest ();
            }

            _db.Entry (promotion).State = EntityState.Modified;
            await _db.SaveChangesAsync ();

            return NoContent ();
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteTodoItem (long id) {
            var promotion = await _db.Promotions.FindAsync (id);

            if (promotion == null) {
                return NotFound ();
            }

            _db.Promotions.Remove (promotion);
            await _db.SaveChangesAsync ();

            return NoContent ();
        }

        [HttpGet ("{id}/city")]
        public async Task<ActionResult<City>> GetPromotionCity (long id) {
            var promotion = await _db.Promotions.Include (x => x.City).FirstOrDefaultAsync (x => x.Id == id);
            // _db.Entry(rider)
            //     .Reference(b => b.City)
            //     .Load();

            if (promotion == null) {
                return NotFound ();
            }

            return promotion.City;
        }
    }
}