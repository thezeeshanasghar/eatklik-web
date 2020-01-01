using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public RestaurantController(Context context, IMapper mapper)
        {
            _db = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAll()
        {
            //var restaurant = await _db.Restaurants.Where(x => x.Status == Status.Enable).ToListAsync();
          var restaurant = await _db.Restaurants.ToListAsync();
            foreach (var rest in restaurant)
            {
                var avgr = await _db.Reviews.Where(c => c.RestaurantId == rest.Id).ToListAsync();
                if(avgr.Count !=0)
                {
                 var avg = avgr.Average(c => c.Rating); 
                 rest.Rating = avg;
                 var count = avgr.Count(); 
                 rest.reviewCount = count;
                }
            }

            return await _db.Restaurants.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetSingle(long id)
        {
            var Restaurant = await _db.Restaurants.FindAsync(id);
           var avg = _db.Reviews.Where(c => c.RestaurantId ==id).Average(c => c.Rating);
             Restaurant.Rating = avg;
           
            if (Restaurant == null)
                return NotFound();

            return Restaurant;
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

        [HttpGet("{id}/extraitem")]
        public async Task<ActionResult<ICollection<RestaurantExtraItem>>> GetRestaurantExtraItem(long id)
        {
            var restaurant = await _db.Restaurants.Include(x => x.RestaurantExtraItems).FirstOrDefaultAsync(x => x.Id == id);
            if (restaurant == null)
                return NotFound();

            return Ok(restaurant.RestaurantExtraItems);
        }

        [HttpGet("{id}/menu")]
        public async Task<ActionResult<ICollection<Menu>>> GetMenus(long id)
        {
            var restaurant = await _db.Restaurants.Include(x => x.RestaurantMenus).FirstOrDefaultAsync(x => x.Id == id);
            if (restaurant == null)
                return NotFound();

            return Ok(restaurant.RestaurantMenus);
        }

        [HttpGet("{id}/restaurant-details")]
        public async Task<ActionResult<Restaurant>> GetRestaurantDetails(long id)
        {
            
                var restaurant = await _db.Restaurants.Include(x => x.RestaurantMenus).Include(x => x.RestaurantCuisines).FirstOrDefaultAsync(x => x.Id == id);
                if (restaurant == null)
                    return NotFound();

                List<Menu> menus = new List<Menu>();
                foreach (var menu in restaurant.RestaurantMenus)
                {
                    var dbMenu = await _db.Menus.Include(x => x.MenuItems).FirstOrDefaultAsync(x => x.Id == menu.Id);
                    menus.Add(dbMenu);
                }
                restaurant.RestaurantMenus = menus;
                List<Cuisine> cuisines = new List<Cuisine>();
                foreach(var restCuisine in restaurant.RestaurantCuisines) {
                    Cuisine cuisine = await _db.Cuisines.Where(x=>x.Id == restCuisine.CuisineId).FirstOrDefaultAsync<Cuisine>();
                    cuisines.Add(cuisine);
                }
               
                return restaurant;

            
        }

          [HttpGet("city/{cityId}")]
        public async Task<ActionResult<ICollection<Restaurant>>> GetRestaurantByCity(int cityId)
        {
            var dbRestaurants = await _db.Restaurants.Where(x => x.CityId == cityId).ToListAsync();
            if (dbRestaurants == null)
                return NotFound();
            return dbRestaurants;

        }

         [HttpGet("{keyword}/search")]
        public async Task<ActionResult<ICollection<Restaurant>>> SearchRestaurant(string keyword)
        {

                    var dbRestaurants = await _db.Restaurants.Where(c => c.Name.ToLower().Contains(keyword.ToLower())).ToListAsync();

                    return dbRestaurants;    
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
        [HttpPut("{id}/status/{status}")]
        public async Task<IActionResult> UpdateRestaurantStatus(long id, int status)
        {
            var dbRestaurant = await _db.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRestaurant == null)
                return NotFound();
            dbRestaurant.Status = (Status) status;
            _db.Entry(dbRestaurant).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var Restaurant = await _db.Restaurants.FindAsync(id);

            if (Restaurant == null)
                return NotFound();

            _db.Restaurants.Remove(Restaurant);
            await _db.SaveChangesAsync();

            return NoContent();
        }

      
    }
}