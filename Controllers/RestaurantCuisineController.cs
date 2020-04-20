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

        [HttpDelete("{resid}/{cuid}")]
        public async Task<IActionResult> Delete(long resid , long cuid)
        {
            var RestaurantCuisine = await _db.RestaurantCuisines.Where(x=> x.RestaurantId == resid && x.CuisineId == cuid).FirstOrDefaultAsync();

            if (RestaurantCuisine == null)
                return NotFound();

            _db.RestaurantCuisines.Remove(RestaurantCuisine);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}