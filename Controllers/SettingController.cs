using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eatklik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace eatklik.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly Context _db;

        public SettingController(Context context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Setting>>> GetAll()
        {
            return await _db.Settings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Setting>> GetSingle(long id)
        {
            var setting = await _db.Settings.FindAsync(id);
            if (setting == null)
                return NotFound();

            return setting;
        }

        [HttpPost]
        public async Task<ActionResult<Setting>> Post(Setting setting)
        {
            _db.Settings.Update(setting);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = setting.Id }, setting);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Setting setting)
        {
            if (id != setting.Id)
                return BadRequest();

            _db.Entry(setting).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var setting = await _db.Settings.FindAsync(id);
            if (setting == null)
                return NotFound();

            _db.Settings.Remove(setting);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("sms/{Contact}/{Type}")]
        public async Task<IActionResult> Sendsms(string Contact,string Type)
        {

            var customer = new Customer();
            var Rider = new Rider();
            
            var Code=0;
            if(Type=="Rider")
            {
                Rider= await _db.Riders.Where(x=>x.MobileNo==Contact).FirstOrDefaultAsync();
                if(Rider!=null)
                {

                 Code=Rider.Code;
                }
                  
            }else{
             
               customer= await _db.Customers.Where(x=>x.MobileNumber==Contact).FirstOrDefaultAsync();
               if(customer!=null)
                {
               ;
                 Code=customer.Code;
                }
            }
           
               if(Rider!=null || customer!=null )
            {
     const string accountSid = "ACd19cd68ad491dc6188b1fe8266a7b754";
        const string authToken = "8d5a3b3b5eac2f17afbdb3d2564c86c4";

        TwilioClient.Init(accountSid, authToken);

        var message = MessageResource.Create(
            body: " your verification code is ="+Code,
            from: new Twilio.Types.PhoneNumber("+14422673664"),
            to: new Twilio.Types.PhoneNumber("+92"+Contact.Substring(1))//"+923365269373"
        );
            }else{
                            return StatusCode(404,"No Record Found");
 
            }

            //       var response="http://api.muxmarketo.com/api/sendsms?";
            //           response+="id=demo";
            //           response+="&pass=demo";
            //           response+="&mobile=923365269373";
            //           response+="&brandname=Eatklik";
            //           response+="&msg=This is demo<br>thanks";
            //           response+="&language=English&network=1";
            // var url= (HttpWebRequest)WebRequest.Create(response);
            // url.GetResponse();
        
          
            return NoContent();
        }

    }
}
