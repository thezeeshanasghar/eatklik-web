using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eatklik.DTOs;
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
        public async Task<Response<IEnumerable<OrderDTO>>> GetAll()
        {
            try
            {
                var dbOrders = await _db.Orders.ToListAsync();
                List<OrderDTO> orders = _mapper.Map<List<OrderDTO>>(dbOrders);
                return new Response<IEnumerable<OrderDTO>>(true, null, orders);

            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<OrderDTO>>(false, ex.Message, null);
            }
        }

        [HttpPost("customer-order")]
        public Response<OrderDTO> PostCustomerOrder(OrderDTO postedOrder)
        {
            try
            {
                Customer customer = _db.Customers.Where(x => x.Id == postedOrder.CustomerId).FirstOrDefault();
                Order order = new Order();
                order = _mapper.Map<Order>(postedOrder);                
                order.Customer = customer;
                order.Created = DateTime.Now;
                order.Status = AppVariables.Pending;
                _db.Orders.Add(order);
                _db.SaveChanges();
                return new Response<OrderDTO>(true, null, postedOrder);

            }
            catch (Exception ex)
            {
                return new Response<OrderDTO>(false, ex.Message, null);
            }
        }

    }
}