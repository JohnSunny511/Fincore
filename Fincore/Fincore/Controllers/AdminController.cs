using Fincore.DBclasses;
using Fincore.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fincore.Controllers
{
    [Route("Admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        AdminDBclss dbobj = new AdminDBclss();
        CustomerDBclss custdb = new CustomerDBclss();
        AccountDBclss accdb = new AccountDBclss();



        private int GetLoginId()
        {
            return Convert.ToInt32(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );
        }

        private string GetUserRole()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value ?? "";
        }




        // Get All Customers
        [HttpGet]
        [Route("getallcustomers")]
        public IActionResult GetAllCustomers()
        {
            var customers = dbobj.GetAllCustomersDB();

            if (customers.Count == 0)
                return NotFound(new { message = "No customers found" });

            return Ok(customers);
        }

        // Get Customer Details
        [HttpGet]
        [Route("getcustomerdetails/{customer_id}")]
        public IActionResult GetCustomerDetails(int customer_id)
        {
            var customer = custdb.GetCustomerDB(customer_id);

            if (customer.customer_id == 0)
                return NotFound(new { message = "Customer not found" });

            return Ok(customer);
        }

        // Get All Accounts
        [HttpGet]
        [Route("getallaccounts")]
        public IActionResult GetAllAccounts()
        {
            var accounts = dbobj.GetAllAccountsDB();

            if (accounts.Count == 0)
                return NotFound(new { message = "No accounts found" });

            return Ok(accounts);
        }

        // Get All Transactions
        [HttpGet]
        [Route("getalltransactions")]
        public IActionResult GetAllTransactions()
        {
            var transactions = dbobj.GetAllTransactionsDB();

            if (transactions.Count == 0)
                return NotFound(new { message = "No transactions found" });

            return Ok(transactions);
        }

        // Get Bank Statistics
        [HttpGet]
        [Route("getbankstatistics")]
        public IActionResult GetBankStatistics()
        {
            var statistics = dbobj.GetBankStatisticsDB();

            return Ok(statistics);
        }

        // Update Account Status
        [HttpPut]
        public IActionResult UpdateAccountStatus(int account_id, AccountStatusDTO dto)
        {
            // Validate account status
            if (dto.account_status != "Active" &&
                dto.account_status != "Blocked")
            {
                return BadRequest(new { message = "Invalid account status" });
            }

            int rows = accdb.UpdateAccountStatusDB(
                account_id,
                dto.account_status,
                GetLoginId(),
                GetUserRole()
            );

            if (rows == 0)
                return NotFound(new { message = "Account not found" });

            return Ok(new
            {
                message = "Account status updated successfully"
            });
        }

    }
}
