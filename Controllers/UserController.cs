
using System.Threading.Tasks;
using eatklik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context _db;

        public UserController(Context context)
        {
            _db = context;
        }
        [HttpPost]
        public async Task<ActionResult<User>> Post(User postedUser)
        {
            _db.Users.Update(postedUser);
            await _db.SaveChangesAsync();
            return postedUser;
        }
           [HttpGet("{UserName}/{Password}")]
        public async Task<ActionResult<User>> Login(string UserName,string Password)
        {

            var User = await _db.Users.FirstOrDefaultAsync(x=>x.UserName==UserName && x.Password == Password);
                 
            if (User == null)
                return NotFound(new { message = "Invalid Email or Password." });

            return User;
        }
    }
}