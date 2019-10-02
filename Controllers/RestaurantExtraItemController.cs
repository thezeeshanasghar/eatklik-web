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
    public class RestaurantExtraItemController : ControllerBase
    {
        private readonly Context _db;

        public RestaurantExtraItemController(Context context)
        {
            _db = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantExtraItem>> GetSingle(long id)
        {
            var RestaurantExtraItem = await _db.RestaurantExtraItems.FindAsync(id);
            if (RestaurantExtraItem == null)
                return NotFound();

            return RestaurantExtraItem;
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantExtraItem>> Post(RestaurantExtraItem RestaurantExtraItem)
        {
            _db.RestaurantExtraItems.Update(RestaurantExtraItem);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = RestaurantExtraItem.Id }, RestaurantExtraItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, RestaurantExtraItem RestaurantExtraItem)
        {
            if (id != RestaurantExtraItem.Id)
                return BadRequest();

            _db.Entry(RestaurantExtraItem).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var RestaurantExtraItem = await _db.RestaurantExtraItems.FindAsync(id);

            if (RestaurantExtraItem == null)
                return NotFound();

            _db.RestaurantExtraItems.Remove(RestaurantExtraItem);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}