using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eatklik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Context _db;
        private readonly IMapper _mapper;

        public OrderController(Context context, IMapper mapper)
        {
            _db = context;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return await _db.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetSingle(int id)
        {

            var dbOrder = await _db.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
            if (dbOrder == null)
                return NotFound();
            return dbOrder;

        }

        [HttpPost("customer-order")]
        public async Task<ActionResult<City>> Post(Order postedOrder)
        {
            Customer customer = _db.Customers.Where(x => x.Id == postedOrder.CustomerId).FirstOrDefault();

            postedOrder.Customer = customer;
            postedOrder.Created = DateTime.Now;
            postedOrder.OrderStatus = OrderStatus.New;
            _db.Orders.Add(postedOrder);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = postedOrder.Id }, postedOrder);
        }

        [HttpPut("{id}/order-status/{status}")]
        public async Task<IActionResult> Put(long id, int status)
        {
            var dbOrder = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbOrder == null)
                return NotFound();
            dbOrder.OrderStatus = (OrderStatus) status;
            _db.Entry(dbOrder).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}