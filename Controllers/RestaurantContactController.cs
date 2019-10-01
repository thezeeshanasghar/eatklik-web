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
    public class RestaurantContactController : ControllerBase
    {
        private readonly Context _db;

        public RestaurantContactController(Context context)
        {
            _db = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantContact>> GetSingle(long id)
        {
            var RestaurantContact = await _db.RestaurantContacts.FindAsync(id);
            if (RestaurantContact == null)
                return NotFound();

            return RestaurantContact;
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantContact>> Post(RestaurantContact RestaurantContact)
        {
            _db.RestaurantContacts.Update(RestaurantContact);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = RestaurantContact.Id }, RestaurantContact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, RestaurantContact RestaurantContact)
        {
            if (id != RestaurantContact.Id)
                return BadRequest();

            _db.Entry(RestaurantContact).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var RestaurantContact = await _db.RestaurantContacts.FindAsync(id);

            if (RestaurantContact == null)
                return NotFound();

            _db.RestaurantContacts.Remove(RestaurantContact);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}