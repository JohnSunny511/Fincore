using Fincore.DBclasses;
using Fincore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fincore.Controllers
{
    [Route("Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        AdminDBclss dbobj = new AdminDBclss();
        CustomerDBclss custdb = new CustomerDBclss();
        AccountDBclss accdb = new AccountDBclss();

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
        [Route("updatestatus/{account_id}")]
        public IActionResult UpdateAccountStatus(int account_id, AccountStatusDTO dto)
        {
            int rows = accdb.UpdateAccountStatusDB(
                account_id,
                dto.account_status
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
