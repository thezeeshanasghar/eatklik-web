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
    public class RestaurantController : ControllerBase
    {
        private readonly Context _db;

        public RestaurantController(Context context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAll()
        {
            return await _db.Restaurants.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetSingle(long id)
        {
            var Restaurant = await _db.Restaurants.FindAsync(id);
            if (Restaurant == null)
                return NotFound();

            return Restaurant;
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> Post(Restaurant Restaurant)
        {
            _db.Restaurants.Update(Restaurant);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Restaurant.Id }, Restaurant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Restaurant Restaurant)
        {
            if (id != Restaurant.Id)
                return BadRequest();

            _db.Entry(Restaurant).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var Restaurant = await _db.Restaurants.FindAsync(id);

            if (Restaurant == null)
                return NotFound();

            _db.Restaurants.Remove(Restaurant);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/location")]
        public async Task<ActionResult<ICollection<RestaurantLocation>>> GetLocations(long id)
        {
            // var restaurant = await _db.Restaurants.FindAsync(id);
            var restaurant = await _db.Restaurants.Include(x => x.RestaurantLocations).FirstOrDefaultAsync(x => x.Id == id);

            if (restaurant == null)
                return NotFound();

            return Ok(restaurant.RestaurantLocations);
        }

        [HttpGet("{id}/contact")]
        public async Task<ActionResult<ICollection<RestaurantContact>>> GetContacts(long id)
        {
            // var restaurant = await _db.Restaurants.FindAsync(id);
            var restaurant = await _db.Restaurants.Include(x => x.RestaurantContacts).FirstOrDefaultAsync(x => x.Id == id);

            if (restaurant == null)
                return NotFound();

            return Ok(restaurant.RestaurantContacts);
        }

        [HttpGet("{id}/timing")]
        public async Task<ActionResult<ICollection<RestaurantTiming>>> GetTimings(long id)
        {
            var restaurant = await _db.Restaurants.Include(x => x.RestaurantTimings).FirstOrDefaultAsync(x => x.Id == id);

            if (restaurant == null)
                return NotFound();

            return Ok(restaurant.RestaurantTimings);
        }

        [HttpGet("{id}/cuisine")]
        public async Task<ActionResult<ICollection<RestaurantCuisine>>> GetCuisines(long id)
        {
            var restaurant = await _db.Restaurants.Include(x => x.RestaurantCuisines).FirstOrDefaultAsync(x => x.Id == id);
            if (restaurant == null)
                return NotFound();

            return Ok(restaurant.RestaurantCuisines);
        }
    }
}