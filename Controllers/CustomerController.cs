using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eatklik.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly Context _db;
        private readonly IMapper _mapper;
        private IHostingEnvironment _env;

        public CustomerController(Context context, IMapper mapper, IHostingEnvironment env)
        {
            _db = context;
            this._mapper = mapper;
            this._env = env;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            return await _db.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetSingle(long id)
        {
            var Customer = await _db.Customers.FindAsync(id);
            if (Customer == null)
                return NotFound();

            return Customer;
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
        public async Task<IActionResult> Delete(long id)
        {
            var Customer = await _db.Customers.FindAsync(id);

            if (Customer == null)
                return NotFound();

            _db.Customers.Remove(Customer);
            await _db.SaveChangesAsync();

            return NoContent();
        }


        #region EK-CUSTOMER

        [HttpPost("login")]
        public async Task<ActionResult<Customer>> Login(Customer postedCustomer)
        {
            var dbCustomer = await _db.Customers.FirstOrDefaultAsync(x => x.Email == postedCustomer.Email
                    && x.Password == postedCustomer.Password);
            if (dbCustomer == null)
                return NotFound(new { message = "Invalid Email or Password." });

            return dbCustomer;
        }

        [HttpGet("{customerId}/orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetCustomerOrders(int customerId)
        {
            var dbCustomer = await _db.Customers.Include(x => x.CustomerOrders).ThenInclude(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == customerId);
            return dbCustomer.CustomerOrders.ToList();
        }

        [HttpPut("edit-profile/{id}")]
        public async Task<ActionResult<Customer>> UpdateProfileAsync(int id)
        {
            var dbCustomer = _db.Customers.FirstOrDefault(x => x.Id == id);
            if (dbCustomer == null)
                return NotFound();

            var httpRequest = HttpContext.Request.Form["Customer"];
            Customer postedCustomer = JsonConvert.DeserializeObject<Customer>(httpRequest);

            string imageName = null;
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var postedFile = HttpContext.Request.Form.Files[0];
                imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                var filePath = Path.Combine(_env.ContentRootPath, "Resources\\Images");

                using (var fileStream = new FileStream(Path.Combine(filePath, imageName), FileMode.Create))
                {
                    await postedFile.CopyToAsync(fileStream);
                    dbCustomer.ImagePath = @"Resources\Images\" + imageName;
                }
            }
            dbCustomer.Name = postedCustomer.Name;
            dbCustomer.MobileNumber = postedCustomer.MobileNumber;
            dbCustomer.Email = postedCustomer.Email;
            dbCustomer.Password = postedCustomer.Password;
            _db.Entry(dbCustomer).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return dbCustomer;

        }

        #endregion  EK-CUSTOMER

    }

}