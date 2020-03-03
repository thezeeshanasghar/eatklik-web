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
    public class CoordinatesController : ControllerBase
    {
        private readonly Context _db;

        public CoordinatesController(Context context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coordinates>>> GetAll()
        {
            return await _db.Coordinates.ToListAsync();
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Coordinates>> GetSingle(long orderId)
        {
            var todoItem = await _db.Coordinates.Where(x=>x.OrderId==orderId).FirstOrDefaultAsync();
            if (todoItem == null)
                return NotFound();

            return todoItem;
        }
       
         [HttpPost]
        public async Task<ActionResult<Coordinates>> Post(Coordinates coordinates)
        {
            _db.Coordinates.Update(coordinates);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { orderId = coordinates.OrderId }, coordinates);
        }

    [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Coordinates coordinates)
        {
            if (id != coordinates.Id)
                return BadRequest();

            _db.Entry(coordinates).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var coordinates = await _db.Coordinates.FindAsync(id);

            if (coordinates == null)
                return NotFound();

            _db.Coordinates.Remove(coordinates);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}