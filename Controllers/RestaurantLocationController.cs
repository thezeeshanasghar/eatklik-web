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
    public class RestaurantLocationController : ControllerBase
    {
        private readonly Context _db;

        public RestaurantLocationController(Context context)
        {
            _db = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantLocation>> GetSingle(long id)
        {
            var RestaurantLocation = await _db.RestaurantLocations.FindAsync(id);
            if (RestaurantLocation == null)
                return NotFound();

            return RestaurantLocation;
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantLocation>> Post(RestaurantLocation RestaurantLocation)
        {
            _db.RestaurantLocations.Update(RestaurantLocation);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = RestaurantLocation.Id }, RestaurantLocation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, RestaurantLocation RestaurantLocation)
        {
            if (id != RestaurantLocation.Id)
                return BadRequest();

            _db.Entry(RestaurantLocation).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var RestaurantLocation = await _db.RestaurantLocations.FindAsync(id);

            if (RestaurantLocation == null)
                return NotFound();

            _db.RestaurantLocations.Remove(RestaurantLocation);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}