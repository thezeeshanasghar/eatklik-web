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
    public class ReviewController : ControllerBase
    {
        private readonly Context _db;

        public ReviewController(Context context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAll()
        {
            return await _db.Reviews.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetSingle(long id)
        {
            var Review = await _db.Reviews.FindAsync(id);
            if (Review == null)
                return NotFound();

            return Review;
        }

        [HttpPost]
        public async Task<ActionResult<Review>> Post(Review Review)
        {
            _db.Reviews.Update(Review);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Review.Id }, Review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Review Review)
        {
            if (id != Review.Id)
                return BadRequest();

            _db.Entry(Review).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var Review = await _db.Reviews.FindAsync(id);

            if (Review == null)
                return NotFound();

            _db.Reviews.Remove(Review);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}