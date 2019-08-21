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

        [HttpPost("customer-order")]
        public Response<OrderDTO> PostCustomerOrder(OrderDTO postedOrder)
        {
            try
            {
                Customer customer = _db.Customers.Where(x => x.Id == postedOrder.CustomerId).FirstOrDefault();
                Order order = new Order();
                order.Customer = customer;
                order = _mapper.Map<Order>(postedOrder);   
                 _db.Orders.Add(order);
                 _db.SaveChanges();
                 //TODO: Problem in creating more than one order
                return new Response<OrderDTO>(true, null, postedOrder);

            }
            catch (Exception ex)
            {
                return new Response<OrderDTO>(false, ex.Message, null);
            }
        }
    }
}