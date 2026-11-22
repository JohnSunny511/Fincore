using Fincore.DBclasses;
using Fincore.Models;
using Fincore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fincore.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        AuthDBclss dbobj = new AuthDBclss();

        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        //Customer Registration
        [HttpPost]
        [Route("customerregister")]
        public IActionResult CustomerRegister([FromBody] CustomerRegisterDTO createdto)
        {
            Customerclss cust = new Customerclss();

            cust.customer_name = createdto.customer_name;
            cust.age = createdto.age;
            cust.email = createdto.email;
            cust.phone = createdto.phone;
            cust.address = createdto.address;
            cust.username = createdto.username;
            cust.password = createdto.password;

            int id = dbobj.CustomerRegisterDB(cust);

            return Ok(new { message = "Customer registered successfully", customer_id = id });
        }


        // Admin Registration
        [HttpPost]
        [Route("adminregister")]
        public IActionResult AdminRegister([FromBody] AdminRegisterDTO createdto)
        {
            Adminclss adm = new Adminclss();

            adm.admin_name = createdto.admin_name;
            adm.admin_email = createdto.admin_email;
            adm.username = createdto.username;
            adm.password = createdto.password;

            int id = dbobj.AdminRegisterDB(adm);

            return Ok(new { message = "Admin registered successfully", admin_id = id });
        }



        // Login
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDTO createdto)
        {
            Loginclss log = new Loginclss();

            log.username = createdto.username;
            log.password = createdto.password;

            Loginclss result = dbobj.LoginDB(log);

            if (result.reg_id != 0)
            {
                return Ok(new
                {
                    message = "Login successful",
                    reg_id = result.reg_id,
                    role = result.role
                });
            }
            else
            {
                return Ok(new { message = "Invalid login" });
            }
        }


        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
