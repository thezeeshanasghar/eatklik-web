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
    public class RestaurantCuisineController : ControllerBase
    {
        private readonly Context _db;

        public RestaurantCuisineController(Context context)
        {
            _db = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(RestaurantCuisine restaurantCuisine)
        {
            var cuisine = await _db.Cuisines.Include(x => x.RestaurantCuisines).FirstOrDefaultAsync(x => x.Id == restaurantCuisine.CuisineId);
            if (cuisine == null)
                return NotFound();
            // _db.Restaurants.RestaurantCuisines.Update(RestaurantCuisine);
            cuisine.RestaurantCuisines.Add(restaurantCuisine);
            await _db.SaveChangesAsync();

            return Ok();
        }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(long id)
        // {
        //     var RestaurantCuisine = await _db.RestaurantCuisines.FindAsync(id);

        //     if (RestaurantCuisine == null)
        //         return NotFound();

        //     _db.RestaurantCuisines.Remove(RestaurantCuisine);
        //     await _db.SaveChangesAsync();

        //     return NoContent();
        // }
    }
}