using Microsoft.AspNetCore.Mvc;
using Fincore.Models;
using Fincore.Models.DTOs;
using Fincore.DBclasses;
using System.Data.SqlClient;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fincore.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        AccountDBclss dbobj = new AccountDBclss();


        // POST api/<AccountsController>
        [Route("createaccount")]
        [HttpPost]
        public IActionResult CreateAcccount(AccountCreateDTO dto)
        {
            Accountclss obj = new Accountclss();
            obj.customer_id = dto.customer_id;
            obj.account_type = dto.account_type;

            int id = dbobj.CreateAccountDB(obj);

            return Ok(new
            {
                message = "Account Created Succesfully",
                account_id = id
            });
            
        }



        [HttpGet]
        [Route("getcustomeraccounts/{customer_id}")]
        public IActionResult GetCustomerAccounts(int customer_id)
        {
            var accounts = dbobj.GetCustomerAccountsDB(customer_id);

            if (accounts.Count == 0)
                return NotFound(new { message = "No accounts found for this customer" });

            return Ok(accounts);
        }



        // Get account details
        [HttpGet]
        [Route("getaccountdetails/{account_id}")]
        public IActionResult GetAccountDetails(int account_id)
        {
            var account = dbobj.GetAccountDetailsDB(account_id);

            if (account.account_id == 0)
                return NotFound(new { message = "Account not found" });

            return Ok(account);
        }


        // Get account balance
        [HttpGet]
        [Route("getaccountbalance/{account_id}")]
        public IActionResult GetAccountBalance(int account_id)
        {
            var balance = dbobj.GetAccountBalanceDB(account_id);

            if (balance == null)
                return NotFound(new { message = "Account not found" });

            return Ok(new
            {
                account_id = account_id,
                balance = balance
            });
        }




        // Update Account Status
        [HttpPut]
        [Route("updatestatus/{account_id}")]
        public IActionResult UpdateAccountStatus(int account_id, AccountStatusDTO dto)
        {
            int rows = dbobj.UpdateAccountStatusDB(account_id, dto.account_status);

            if (rows == 0)
                return NotFound(new { message = "Account not found" });

            return Ok(new { message = "Account status updated successfully" });
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
