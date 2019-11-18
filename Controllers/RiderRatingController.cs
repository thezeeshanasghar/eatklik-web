using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eatklik.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eatklik.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RiderRatingController : ControllerBase
    {
        private readonly Context _db;
        private IHostingEnvironment _env;

        public RiderRatingController(Context context, IHostingEnvironment env)
        {
            _db = context;
            this._env = env;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RiderRating>> GetSingle(long id)
        {
            var todoItem = await _db.RiderRatings.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<RiderRating>> Post(RiderRating riderRating)
        {
            _db.RiderRatings.Update(riderRating);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = riderRating.Id }, riderRating);
        }

    }
}