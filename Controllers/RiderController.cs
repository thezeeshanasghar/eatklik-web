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
            var todoItem = await _db.Riders.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<Rider>> Post(Rider rider)
        {
            _db.Riders.Update(rider);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = rider.Id }, rider);
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
                _db.Entry(dbRider).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return dbRider;
           
        }
    }
}