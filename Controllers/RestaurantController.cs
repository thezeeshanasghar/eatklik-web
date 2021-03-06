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
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAll([FromQuery] string open = "", [FromQuery] string top = "", [FromQuery] String cuisineId ="")
        {
        List<Restaurant> restaurantsC = new List<Restaurant>();
            if (!String.IsNullOrEmpty(open))
            {
                var restaurantOpen = await _db.Restaurants.Where(x => x.Status == Status.Enable).ToListAsync();
                 if (!String.IsNullOrEmpty(top))
                {
                var restaurantTop = restaurantOpen.OrderByDescending(x=> x.Rating).ToList();
                return restaurantTop;
                }

                return restaurantOpen;
            }
            else if (!String.IsNullOrEmpty(top))
            {
                var restaurant = await _db.Restaurants.OrderByDescending(x=> x.Rating).ToListAsync();
                return restaurant;
            }
            else if ((!String.IsNullOrEmpty(cuisineId)))
            {
                var cusineId = Int32.Parse(cuisineId);
                 var restaurant = await _db.Restaurants.Include(x=>x.RestaurantCuisines).ToListAsync();
                 foreach (var rest in restaurant)
                 { 
                     var Crestaurant = rest.RestaurantCuisines.Where(x=>x.CuisineId == cusineId).FirstOrDefault();
                     if (Crestaurant != null)
                     restaurantsC.Add(rest);
                 }
                 return restaurantsC;
            }
            else {
           var restaurant = await _db.Restaurants.OrderBy(x=>x.Id).ToListAsync();
            // foreach (var rest in restaurant)
            // {
            //     var avgr = await _db.Reviews.Where(c => c.RestaurantId == rest.Id).ToListAsync();
            //     if(avgr.Count !=0)
            //     {
            //      var avg = avgr.Average(c => c.Rating); 
            //      rest.Rating = avg;
            //      var count = avgr.Count(); 
            //      rest.reviewCount = count;
            //     }
            // }
            return restaurant;
            
            }
        }
           
        [HttpGet("city/{cityId}")]
        public async Task<ActionResult<ICollection<Restaurant>>> GetRestaurantByCity(int cityId,[FromQuery] string open = "", [FromQuery] string top = "", [FromQuery] String cuisineId ="")
        {
            List<Restaurant> restaurantsC = new List<Restaurant>();
            if (!String.IsNullOrEmpty(open))
            {
                var restaurantOpen = await _db.Restaurants.Where(x =>x.CityId == cityId && x.Status == Status.Enable).ToListAsync();
                 if (!String.IsNullOrEmpty(top))
                {
                var restaurantTop = restaurantOpen.OrderByDescending(x=> x.Rating).ToList();
                return restaurantTop;
                }

                return restaurantOpen;
            }
            else if (!String.IsNullOrEmpty(top))
            {
                var restaurant = await _db.Restaurants.Where(x=>x.CityId == cityId).OrderByDescending(x=> x.Rating).ToListAsync();
                return restaurant;
            }
            else if ((!String.IsNullOrEmpty(cuisineId)))
            {
                var cusineId = Int32.Parse(cuisineId);
                 var restaurant = await _db.Restaurants.Include(x=>x.RestaurantCuisines).Where(x=>x.CityId == cityId).ToListAsync();
                 foreach (var rest in restaurant)
                 { 
                     var Crestaurant = rest.RestaurantCuisines.Where(x=>x.CuisineId == cusineId).FirstOrDefault();
                     if (Crestaurant != null)
                     restaurantsC.Add(rest);
                 }
                 return restaurantsC;
            }
            else {
           var restaurant = await _db.Restaurants.Where(x=> x.CityId == cityId).OrderBy(x=>x.Id).ToListAsync();
            return restaurant;
            }

        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetSingle(long id)
        {
            var Restaurant = await _db.Restaurants.FindAsync(id);
           var avgr = await _db.Reviews.Where(c => c.RestaurantId ==id).OrderBy(x=>x.Id).ToListAsync();
           if(avgr.Count !=0 )
           {
           var avg = avgr.Average(c => c.Rating);
             Restaurant.Rating = avg;
           }
           
            if (Restaurant == null)
                return NotFound();

            return Restaurant;
        }

         [HttpGet("{lat}/{lng}")]
        public async Task<ActionResult<ICollection<Restaurant>>> GetInField(float lat , float lng , [FromQuery] string open = "", [FromQuery] string top = "", [FromQuery] String cuisineId ="")
        {
             List<Restaurant> restaurant = new List<Restaurant>();
             List<Restaurant> restaurantC = new List<Restaurant>();
             var RestaurantLocation = await _db.RestaurantLocations.ToListAsync();
            foreach (var loc in RestaurantLocation)
            {
                var distance = CalculateDistance(lat , lng , loc.Latitude , loc.Longitude);
                if (distance <= loc.DelRadius)
                {
                var rest = await _db.Restaurants.Include(x=>x.RestaurantCuisines).Where(x=> x.Id == loc.RestaurantId).FirstOrDefaultAsync();
                rest.approximateTime = Convert.ToInt32((5 * distance));
               restaurant.Add(rest);
                }

            }
         if (!String.IsNullOrEmpty(open))
            {
                var restaurantOpen =  restaurant.Where(x=>x.Status == Status.Enable).ToList();
                 if (!String.IsNullOrEmpty(top))
                {
                var restaurantTop = restaurantOpen.OrderByDescending(x=> x.Rating).ToList();
                return restaurantTop;
                }

                return restaurantOpen;
            }
             
             else if (!String.IsNullOrEmpty(top))
            {
                var restaurantT =  restaurant.OrderByDescending(x=> x.Rating).ToList();
                return restaurantT;
            }
             else if ((!String.IsNullOrEmpty(cuisineId)))
            {
                var cusineId = Int32.Parse(cuisineId);
               //  var restaurantC = restaurant.Include(x=>x.RestaurantCuisines).Where(x=>x.CityId == cityId).ToListAsync();
                 foreach (var rest in restaurant)
                 { 
                     var Crestaurant = rest.RestaurantCuisines.Where(x=>x.CuisineId == cusineId).FirstOrDefault();
                     if (Crestaurant != null)
                     restaurantC.Add(rest);
                 }
                 return restaurantC;
            }
            else  
            return restaurant;
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

       

         [HttpGet("{keyword}/search")]
        public async Task<ActionResult<ICollection<Restaurant>>> SearchRestaurant(string keyword)
        {

                    var dbRestaurants = await _db.Restaurants.Where(c => c.Name.ToLower().Contains(keyword.ToLower())).ToListAsync();

                    return dbRestaurants;    
        }

        [HttpGet("sponsor")]
        public async Task<ActionResult<ICollection<Restaurant>>> GetSponsorRestaurant()
        {

                    var dbRestaurants = await _db.Restaurants.Where(c => c.IsSponsor == true).ToListAsync();

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

          [HttpPut("{id}/sponsor/{status}")]
        public async Task<IActionResult> UpdateRestaurantSponsor(long id, bool status)
        {
            var dbRestaurant = await _db.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRestaurant == null)
                return NotFound();
            dbRestaurant.IsSponsor = status;
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

     public static double CalculateDistance(double sLatitude,double sLongitude, double eLatitude, 
                               double eLongitude)
{
    var radiansOverDegrees = (Math.PI / 180.0);
    var sLatitudeRadians = sLatitude * radiansOverDegrees;
    var sLongitudeRadians = sLongitude * radiansOverDegrees;
    var eLatitudeRadians = eLatitude * radiansOverDegrees;
    var eLongitudeRadians = eLongitude * radiansOverDegrees;

    var dLongitude = eLongitudeRadians - sLongitudeRadians;
    var dLatitude = eLatitudeRadians - sLatitudeRadians;

    var result1 = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) + 
                  Math.Cos(sLatitudeRadians) * Math.Cos(eLatitudeRadians) * 
                  Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

    // Using 3956 as the number of miles around the earth
    var result2 = 3956.0 * 2.0 * 
                  Math.Atan2(Math.Sqrt(result1), Math.Sqrt(1.0 - result1));

    return result2;
}

      
    }
}