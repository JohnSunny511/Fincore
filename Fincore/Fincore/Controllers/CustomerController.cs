using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Fincore.DBclasses;    

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fincore.Controllers
{
    [Route("customer")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        CustomerDBclss dbobj = new CustomerDBclss();

        // GET: api/<CustomerController>
        [HttpGet("getcustomer/{id}")]
        public IActionResult GetCustomer(int id)
        {
            int customer_id = Convert.ToInt32(
                User.FindFirst("reg_id")?.Value
            );

            if (id != customer_id)
            {
                return Forbid();
            }

            var data = dbobj.GetCustomerDB(id);

            if (data.customer_id == 0)
            {
                return NotFound("Customer not found");
            }

            return Ok(data);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
