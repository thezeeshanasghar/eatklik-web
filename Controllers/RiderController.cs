using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eatklik.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RiderController : ControllerBase
    {
        private readonly Context _db;
        private IHostingEnvironment _env;
        public RiderController(Context context, IHostingEnvironment env)
        {
            _db = context;
            this._env = env;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rider>>> GetAll()
        {
            return await _db.Riders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rider>> GetSingle(long id)
        {
            var rider = await _db.Riders.FindAsync(id);
            if (rider == null)
                return NotFound();


            var ratings = await _db.RiderRatings.Where(x => x.RiderId == rider.Id).ToListAsync();
            if (ratings == null)
            {
                rider.Rating = 0;
            }
            else
            {
                float sum = 0;
                float i = ratings.Count();
                foreach (var rating in ratings)
                {
                    sum = sum + rating.Value;
                }
                float average = sum / i;

                rider.Rating = average;
            }

            return rider;
        }

        [HttpPost]
        public async Task<ActionResult<Rider>> Post(Rider rider)
        {
             var verify=_db.Riders.Where(x=>x.MobileNo==rider.MobileNo).ToList();
            if(verify.Count>0)
            {
            return StatusCode(404,"Contact Already Exist");

            }else{
                       Random random = new Random();
            rider.Code=random.Next(9999);
            rider.IsVerified=0;

            _db.Riders.Update(rider);
            await _db.SaveChangesAsync();
            }
//------------------------------------------------

    // UserAuthentication UserAuthentication=new UserAuthentication();
    //          Random random = new Random();
    //          UserAuthentication.Code= random.Next(99999).ToString();
    //          UserAuthentication.PersonId=rider.Id;
    //          UserAuthentication.IsVerified=0;
    //          UserAuthentication.Type="Rider";
    //         _db.UserAuthentication.Update(UserAuthentication);
    //         await _db.SaveChangesAsync();

//-------------------------------------------------
            return CreatedAtAction(nameof(GetSingle), new { id = rider.Id }, rider);
        }


        [HttpPut("VerifyRider/{id}/{Code}")]
        public async Task<ActionResult<Rider>> VerifyRider(int id,int Code)
        {
            
                 Random random = new Random();
        Console.WriteLine(Code);
              var dbVerify = await _db.Riders.FirstOrDefaultAsync(x => x.Id == id );
            if (dbVerify == null || dbVerify.Code!=Code)
                return NotFound();
            dbVerify.IsVerified = 1;
            dbVerify.Code=random.Next(9999);
            _db.Entry(dbVerify).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            var dbRider = await _db.Riders.FirstOrDefaultAsync(x => x.Id == id );
            return dbRider;
        }
        [HttpPost("login")]
        public ActionResult<Rider> Login(Rider postedRider)
        {
            var dbRider = _db.Riders.FirstOrDefault(x => x.MobileNo == postedRider.MobileNo
            && x.Password == postedRider.Password);
            if (dbRider == null)
                return NotFound();

            if (dbRider.ProfileImage == null)
            {
                // var imgPath = Directory.GetFiles("~/Content/RiderImages/avatar.png");
                dbRider.ProfileImage = "avatar.png";
            }
            return dbRider;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Rider rider)
        {
            if (id != rider.Id)
                return BadRequest();

            _db.Entry(rider).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var name = await _db.Riders.FindAsync(id);
            if (name == null)
                return NotFound();

            _db.Riders.Remove(name);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/city")]
        public async Task<ActionResult<City>> GetRiderCity(long id)
        {
            var rider = await _db.Riders.Include(x => x.City).FirstOrDefaultAsync(x => x.Id == id);
            if (rider == null)
                return NotFound();

            return rider.City;
        }



        [HttpPut("edit-profile/{id}")]
        public async Task<ActionResult<Rider>> UpdateProfileAsync(int id)
        {
            var dbRider = _db.Riders.FirstOrDefault(x => x.Id == id);
            if (dbRider == null)
                return NotFound();

            var httpRequest = HttpContext.Request.Form["rider"];
            Rider postedRider = JsonConvert.DeserializeObject<Rider>(httpRequest);

            string imageName = null;
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var postedFile = HttpContext.Request.Form.Files[0];
                imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                var filePath = Path.Combine(_env.ContentRootPath, "Content\\RiderImages");

                using (var fileStream = new FileStream(Path.Combine(filePath, imageName), FileMode.Create))
                {
                    await postedFile.CopyToAsync(fileStream);
                    dbRider.ProfileImage = imageName;
                }
            }
            dbRider.Name = postedRider.Name;
            dbRider.MobileNo = postedRider.MobileNo;
            dbRider.Password = postedRider.Password;
            dbRider.CNIC = postedRider.CNIC;
            dbRider.Address = postedRider.Address;
            _db.Entry(dbRider).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return dbRider;

        }


        [HttpGet("{id}/rating")]
        public async Task<ActionResult<ICollection<RiderRating>>> GetRatings(long id)
        {
            // var restaurant = await _db.Restaurants.FindAsync(id);
            var rider = await _db.Riders.Include(x => x.RiderRatings).FirstOrDefaultAsync(x => x.Id == id);

            if (rider == null)
                return NotFound();

            return Ok(rider.RiderRatings);
        }

        [HttpGet("city/{cityId}")]
        public async Task<ActionResult<ICollection<Rider>>> GetRiderByCity(long cityId)
        {
            var dbRiders = await _db.Riders.Where(x => x.CityId == cityId).ToListAsync();
            if (dbRiders == null)
                return NotFound();
            foreach (var rider in dbRiders)
            {
                var ratings = await _db.RiderRatings.Where(x => x.RiderId == rider.Id).ToListAsync();
                float sum = 0;
                float i = ratings.Count();
                foreach (var rating in ratings)
                {
                    sum = sum + rating.Value;
                }
                float average = sum / i;
                rider.Rating = average;
            }

            return dbRiders;
        }
        
     [HttpGet("VerifyNumber/{Contact}")]
        public async Task<ActionResult<Rider>> VerifyContact(string Contact)
        {
            var dbRider = await _db.Riders.FirstOrDefaultAsync(x => x.MobileNo == Contact);
            return dbRider;
        }
    }
}