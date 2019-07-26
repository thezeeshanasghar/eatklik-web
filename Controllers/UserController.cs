
using System.Threading.Tasks;
using eatklik.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}