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
    public class CouponCodeController : ControllerBase
    {
        private readonly Context _db;

        public CouponCodeController(Context context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CouponCode>>> GetAll()
        {
            return await _db.CouponCodes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CouponCode>> GetSingle(long id)
        {
            var todoItem = await _db.CouponCodes.FindAsync(id);
            if (todoItem == null)
                return NotFound();

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<CouponCode>> Post(CouponCode couponCode)
        {
            _db.CouponCodes.Update(couponCode);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = couponCode.Id }, couponCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, CouponCode couponCode)
        {
            if (id != couponCode.Id)
                return BadRequest();

            _db.Entry(couponCode).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var couponCode = await _db.CouponCodes.FindAsync(id);

            if (couponCode == null)
                return NotFound();

            _db.CouponCodes.Remove(couponCode);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}