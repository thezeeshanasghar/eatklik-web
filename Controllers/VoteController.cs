
using System.Threading.Tasks;
using eatklik.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly Context _db;

        public VoteController(Context context)
        {
            _db = context;
        }
       
       [HttpGet]
        public async Task<ActionResult<IEnumerable<Vote>>> GetAll()
        {
            return await _db.Votes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vote>> GetSingle(long id)
        {
            var todoItem = await _db.Votes.FindAsync(id);
            if (todoItem == null)
                return NotFound();

            return todoItem;
        }
        [HttpPost]
        public async Task<ActionResult<Vote>> Post(Vote postedVote)
        {
            _db.Votes.Update(postedVote);
            await _db.SaveChangesAsync();
            return postedVote;
        }
    }
}