using Microsoft.AspNetCore.Mvc;
using Fincore.DBclasses;    

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fincore.Controllers
{
    [Route("customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        CustomerDBclss dbobj = new CustomerDBclss();

        // GET: api/<CustomerController>
        [HttpGet]
        [Route("getcustomer/{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = dbobj.GetCustomerDB(id);
            if(customer.customer_id == 0)
            {
                return NotFound(new { message = "Customer not found" });
            }
            return Ok(customer);
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
