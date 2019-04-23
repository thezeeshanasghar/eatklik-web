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
    public class RestaurantTimingController : ControllerBase
    {
        private readonly Context _db;

        public RestaurantTimingController(Context context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantTiming>>> GetAll()
        {
            return await _db.RestaurantTimings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantTiming>> GetSingle(long id)
        {
            var RestaurantTiming = await _db.RestaurantTimings.FindAsync(id);
            if (RestaurantTiming == null)
                return NotFound();

            return RestaurantTiming;
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantTiming>> Post(RestaurantTiming RestaurantTiming)
        {
            _db.RestaurantTimings.Update(RestaurantTiming);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = RestaurantTiming.Id }, RestaurantTiming);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, RestaurantTiming RestaurantTiming)
        {
            if (id != RestaurantTiming.Id)
                return BadRequest();

            _db.Entry(RestaurantTiming).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var RestaurantTiming = await _db.RestaurantTimings.FindAsync(id);

            if (RestaurantTiming == null)
                return NotFound();

            _db.RestaurantTimings.Remove(RestaurantTiming);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}