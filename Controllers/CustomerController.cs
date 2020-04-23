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
        
        // [HttpGet("city/{cityId}")]
        // public async Task<ActionResult<ICollection<Customer>>> GetCustomerByCity(int cityId)
        // {
        //     var dbCustomers = await _db.Customers.Where(x => x.CityId == cityId).ToListAsync();
        //     if (dbCustomers == null)
        //         return NotFound();
        //     return dbCustomers;

        // }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post(Customer Customer)
        {
         Random random = new Random();

             Customer.Code= random.Next(9999);
             Customer.IsVerified=0;
            _db.Customers.Update(Customer);
           
            await _db.SaveChangesAsync();
// //------------------------------------------------

//     UserAuthentication UserAuthentication=new UserAuthentication();
//              Random random = new Random();
//              UserAuthentication.Code= random.Next(99999).ToString();
//              UserAuthentication.PersonId=Customer.Id;
//              UserAuthentication.IsVerified=0;
//              UserAuthentication.Type="Customer";
//             _db.UserAuthentication.Update(UserAuthentication);
//             await _db.SaveChangesAsync();

// //-------------------------------------------------
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

            var dbCustomer = await _db.Customers.FirstOrDefaultAsync(x=>x.MobileNumber==postedCustomer.MobileNumber && x.Password == postedCustomer.Password && x.Status==Status.Enable);//c(x => x.Email == postedCustomer.Email
                 
            if (dbCustomer == null)
                return NotFound(new { message = "Invalid Mobile Number or Password." });

            return dbCustomer;
        }


        [HttpPut("VerifyCustomer/{id}")]
        public async Task<ActionResult<Customer>> VerifyCustomer(int id,string Code)
        {
                 Random random = new Random();

              var dbVerify = await _db.Customers.FirstOrDefaultAsync(x =>  x.Id==id  );
          
            if (dbVerify == null && dbVerify.Code==int.Parse(Code)){
                        Console.WriteLine(dbVerify.Code);

              return NotFound();

            }
              
            dbVerify.IsVerified = 1;
            dbVerify.Code= random.Next(9999);

            _db.Entry(dbVerify).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            var dbCustomer = await _db.Customers.FirstOrDefaultAsync(x => x.Id == id );
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

     [HttpGet("VerifyNumber/{Contact}")]
        public async Task<ActionResult<Customer>> VerifyContact(string Contact)
        {
            var dbCustomer = await _db.Customers.FirstOrDefaultAsync(x => x.MobileNumber == Contact);
            return dbCustomer;
        }


        #endregion  EK-CUSTOMER

    }

}