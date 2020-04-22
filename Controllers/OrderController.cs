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

        public OrderController(Context context)
        {
            _db = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return await _db.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetSingle(int id)
        {

            var dbOrder = await _db.Orders.Include(x=>x.Restaurant).ThenInclude(x=>x.RestaurantLocations).Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
            if (dbOrder == null)
                return NotFound();
            return dbOrder;

        }

        [HttpGet("rider/{id}")]
        public async Task<ActionResult<ICollection<Order>>> GetOrdersByRider(int id)
        {

            var dbOrder = await _db.Orders.Where(x => x.RiderId == id).ToListAsync();
            if (dbOrder == null)
                return NotFound();
            return dbOrder;

        }

          [HttpGet("rider-complete/{id}")]
        public async Task<ActionResult<ICollection<Order>>> GetCompleteOrdersByRider(int id)
        {

            var dbOrder = await _db.Orders.Where(x => x.RiderId == id && x.OrderStatus == OrderStatus.Complete).ToListAsync();
            if (dbOrder == null)
                return NotFound();
            return dbOrder;

        }

        // [HttpGet("rider/{id}/new")]
        // public async Task<ActionResult<ICollection<Order>>> GetNewOrdersByRider(int id)
        // {

        //     var dbOrder = await _db.Orders.Where(x => x.RiderId == id && x.OrderStatus == OrderStatus.Active && x.RiderStatus == RiderStatus.New).ToListAsync();
        //     if (dbOrder == null)
        //         return NotFound();
        //     return dbOrder;

        // }

        // [HttpGet("rider/{id}/pending")]
        // public async Task<ActionResult<ICollection<Order>>> GetPendingOrdersByRider(int id)
        // {

        //     var dbOrder = await _db.Orders.Where(x => x.RiderId == id && x.OrderStatus == OrderStatus.Active && x.RiderStatus == RiderStatus.Accepted).ToListAsync();
        //     if (dbOrder == null)
        //         return NotFound();
        //     return dbOrder;

        // }

         [HttpGet("rider/{id}/new")]
        public async Task<ActionResult<ICollection<Order>>> GetNewOrdersByRider(int id)
        {

            var dbOrder = await _db.Orders.Include(x=>x.Restaurant).ThenInclude(x=>x.RestaurantLocations).Where(x => x.RiderId == id && x.OrderStatus == OrderStatus.Assigned).ToListAsync();
            if (dbOrder == null)
                return NotFound();
            return dbOrder;

        }

        [HttpGet("rider/{id}/pending")]
        public async Task<ActionResult<ICollection<Order>>> GetPendingOrdersByRider(int id)
        {

            var dbOrder = await _db.Orders.Include(x=>x.Restaurant).ThenInclude(x=>x.RestaurantLocations).Where(x => x.RiderId == id && x.OrderStatus == OrderStatus.RiderAccepted).ToListAsync();
            if (dbOrder == null)
                return NotFound();
            return dbOrder;

        }

        [HttpPost("customer-order")]
        public async Task<ActionResult<Order>> Post(Order postedOrder)
        {
            Customer customer = _db.Customers.Where(x => x.Id == postedOrder.CustomerId).FirstOrDefault();

            postedOrder.Customer = customer;
            postedOrder.Created = DateTime.Now;
            postedOrder.OrderStatus = OrderStatus.New;
         //   postedOrder.RiderStatus = RiderStatus.New;
            _db.Orders.Update(postedOrder);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSingle), new { id = postedOrder.Id }, postedOrder);
        }

        [HttpPut("{id}/order-status/{status}")]
        public async Task<IActionResult> UpdateOrderStatus(long id, int status)
        {
            var dbOrder = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbOrder == null)
                return NotFound();
            dbOrder.OrderStatus = (OrderStatus) status;
            _db.Entry(dbOrder).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }


        //  [HttpPut("{id}/rider-status/{status}")]
        // public async Task<IActionResult> UpdateRiderStatus(long id, int status)
        // {
        //     var dbOrder = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
        //     if (dbOrder == null)
        //         return NotFound();
        //     dbOrder.RiderStatus = (RiderStatus) status;
        //     _db.Entry(dbOrder).State = EntityState.Modified;
        //     await _db.SaveChangesAsync();

        //     return NoContent();
        // }

        [HttpPut("{id}/order-rider/{rid}")]
        public async Task<IActionResult> UpdateOrderRider(long id, int rid)
        {
            var dbOrder = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbOrder == null)
                return NotFound();

            var dbRider = await _db.Riders.FirstOrDefaultAsync(x => x.Id == rid);
            if (dbRider == null)
                return NotFound();

            dbOrder.RiderId = rid;
            dbOrder.Rider = dbRider;

            _db.Entry(dbOrder).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("city/{cityId}")]
        public async Task<ActionResult<ICollection<Order>>> GetOrderByCity(long cityId)
        {
            var dbOrders = await _db.Orders.Where(x => x.CityId == cityId).ToListAsync();
            if (dbOrders == null)
                return NotFound();
            return dbOrders;

        }
         [HttpGet("{id}/status")]
        public async Task<ActionResult<OrderStatus>> GetSingleStatus(int id)
        {

            var dbOrder = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbOrder == null)
                return NotFound();
            return dbOrder.OrderStatus;

        }
        
        [HttpGet("status/{orderStatus}")]
        public async Task<ActionResult<ICollection<Order>>> GetOrderByStatus(int orderStatus)
        {
            var dbOrders = await _db.Orders.Where(x => x.OrderStatus == (OrderStatus)orderStatus).ToListAsync();
            if (dbOrders == null)
                return NotFound();
            return dbOrders;
        }
        
        [HttpGet("city/{cityId}/status/{orderStatus}")]
        public async Task<ActionResult<ICollection<Order>>> GetOrderByCityAndStatus(int cityId, int orderStatus)
        {
            var dbOrders = await _db.Orders.Where(x => x.CityId == cityId && x.OrderStatus == (OrderStatus)orderStatus).ToListAsync();
            if (dbOrders == null)
                return NotFound();
            return dbOrders;
        }
            [HttpGet("OrderItem/{OrderId}")]
        public async Task<ActionResult<ICollection<OrderItem>>> GetOrderItem(int OrderId)
        {
            var dbOrders = await _db.OrderItems.Where(x => x.Order.Id == OrderId).ToListAsync();
            if (dbOrders == null)
                return NotFound();
            return dbOrders;
        }
          

    }
}