using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eatklik.Models;
using eatklik.Models.Derived;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly Context _db;

        public DashboardController(Context context)
        {
            _db = context;
        }

        [HttpGet]
        public ActionResult<Models.Derived.Dashboard> GetAll()
        {
            var dashboard = new Models.Derived.Dashboard()
            {
                CityCount= _db.Cities.Count(x=> x.Status == Status.Enable),
                CuisineCount=_db.Cities.Count(),
                CustomerCount=_db.Customers.Count(x=>x.Status == Status.Enable),
                PromotionCount = _db.Promotions.Count(),
                RestaurantCount = _db.Restaurants.Count(),
                ReviewCount = _db.Reviews.Count(x=>x.Status==Status.Enable),
                RiderCount = _db.Riders.Count(x=>x.Status == Status.Enable)

            };
            return dashboard;
        }
    }
}