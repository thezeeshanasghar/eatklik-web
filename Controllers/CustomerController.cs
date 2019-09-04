using System;
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
    public class CustomerController : ControllerBase
    {
        private readonly Context _db;

        public CustomerController(Context context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            return await _db.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Response<Customer>> GetSingle(long id)
        {
            var Customer = await _db.Customers.FindAsync(id);
            if (Customer == null)
                return new Response<Customer>(false, "Invalid Email or Password.", null);
            return new Response<Customer>(true, null, Customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post(Customer Customer)
        {
            _db.Customers.Update(Customer);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Customer.Id }, Customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Customer Customer)
        {
            if (id != Customer.Id)
                return BadRequest();

            _db.Entry(Customer).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var Customer = await _db.Customers.FindAsync(id);

            if (Customer == null)
                return NotFound();

            _db.Customers.Remove(Customer);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("login")]
        public Response<Customer> Login(Customer postedCustomer)
        {
            try
            {
                var dbCustomer = _db.Customers.FirstOrDefault(x => x.Email == postedCustomer.Email
                && x.Password == postedCustomer.Password);
                if (dbCustomer == null)
                    return new Response<Customer>(false, "Invalid Email or Password.", null);
                return new Response<Customer>(true, null, dbCustomer);
            }
            catch (Exception ex)
            {
                return new Response<Customer>(false, ex.Message, null);
            }
        }
    }

}