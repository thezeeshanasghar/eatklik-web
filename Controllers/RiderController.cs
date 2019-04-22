using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eatklik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class RiderController : ControllerBase {
        private readonly Context _db;

        public RiderController (Context context) {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rider>>> GetAll () {
            return await _db.Riders.ToListAsync ();
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Rider>> GetSingle (long id) {
            var todoItem = await _db.Riders.FindAsync (id);

            if (todoItem == null) {
                return NotFound ();
            }

            return todoItem;
        }
        
        [HttpPost]
        public async Task<ActionResult<Rider>> Post (Rider city) {
            _db.Riders.Add (city);
            await _db.SaveChangesAsync ();

            return CreatedAtAction (nameof (GetSingle), new { id = city.Id }, city);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Put (long id, Rider Name) {
            if (id != Name.Id) {
                return BadRequest ();
            }

            _db.Entry (Name).State = EntityState.Modified;
            await _db.SaveChangesAsync ();

            return NoContent ();
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteTodoItem (long id) {
            var name = await _db.Riders.FindAsync (id);

            if (name == null) {
                return NotFound ();
            }

            _db.Riders.Remove (name);
            await _db.SaveChangesAsync ();

            return NoContent ();
        }

        [HttpGet ("{id}/city")]
        public async Task<ActionResult<City>> GetRiderCity (long id) {
            var rider = await _db.Riders.Include (x => x.City).FirstOrDefaultAsync (x => x.Id == id);
            // _db.Entry(rider)
            //     .Reference(b => b.City)
            //     .Load();

            if (rider == null) {
                return NotFound ();
            }

            return rider.City;
        }
    }
}