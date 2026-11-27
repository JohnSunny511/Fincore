using Fincore.DBclasses;
using Fincore.Models;
using Fincore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fincore.Controllers
{
    [Route("Transaction")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {

        TransactionDBclss dbobj = new TransactionDBclss();


        AccountDBclss accdb = new AccountDBclss();

        private int GetCustomerId()
        {
            return Convert.ToInt32(User.FindFirst("reg_id")?.Value);
        }


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


        // Deposit
        [HttpPost]
        [Route("deposit")]
        [Authorize(Roles = "Customer")]
        public IActionResult Deposit(DepositDTO dto)
        {
            
            Transactionclss obj = new Transactionclss();

            obj.to_account_id = dto.account_id;
            obj.amount = dto.amount;
            obj.description = dto.description;

            if (!accdb.IsAccountOwnedByCustomerDB(dto.account_id, GetCustomerId()))
            {
                return Forbid();
            }


            int transaction_id = dbobj.DepositDB(obj, GetLoginId(), GetUserRole());

            return Ok(new
            {
                message = "Deposit successful",
                transaction_id = transaction_id
            });
        }


        // Withdrawal
        [HttpPost]
        [Route("withdraw")]
        [Authorize(Roles = "Customer")]
        public IActionResult Withdraw(WithdrawalDTO dto)
        {
            Transactionclss obj = new Transactionclss();

            obj.from_account_id = dto.account_id;
            obj.amount = dto.amount;
            obj.description = dto.description;

            if (!accdb.IsAccountOwnedByCustomerDB(
                dto.account_id, GetCustomerId()))
            {
                return Forbid();
            }

            try
            {
                int transaction_id = dbobj.WithdrawDB(obj, GetLoginId(), GetUserRole());

                return Ok(new
                {
                    message = "Withdrawal successful",
                    transaction_id = transaction_id
                });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50002)
                {
                    return Conflict(new
                    {
                        message = "Withdrawal failed. Account may be inactive or have insufficient balance."
                    });
                }

                return StatusCode(500, new
                {
                    message = "An unexpected database error occurred."
                });
            }
        }


        // Transfer
        [HttpPost]
        [Route("transfer")]
        [Authorize(Roles = "Customer")]
        public IActionResult Transfer(TransferDTO dto)
        {
            Transactionclss obj = new Transactionclss();

            obj.from_account_id = dto.from_account_id;
            obj.to_account_id = dto.to_account_id;
            obj.amount = dto.amount;
            obj.description = dto.description;

            if (!accdb.IsAccountOwnedByCustomerDB(
                dto.from_account_id, GetCustomerId()))
            {
                return Forbid();
            }

            try
            {
                int transaction_id = dbobj.TransferDB(obj, GetLoginId(), GetUserRole());

                return Ok(new
                {
                    message = "Transfer successful",
                    transaction_id = transaction_id
                });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50003)
                {
                    return Conflict(new
                    {
                        message = "Transfer failed. Check account status, balance and transfer details."
                    });
                }

                return StatusCode(500, new
                {
                    message = "An unexpected database error occurred."
                });
            }
        }



        // Get Account Transactions
        [HttpGet]
        [Route("getaccounttransactions/{account_id}")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetAccountTransactions(int account_id)
        {
            if (!accdb.IsAccountOwnedByCustomerDB(account_id, GetCustomerId()))
            {
                return Forbid();
            }
            var transactions = dbobj.GetAccountTransactionsDB(account_id);

            if (transactions.Count == 0)
                return NotFound(new { message = "No transactions found for this account" });

            return Ok(transactions);
        }


        // Get Transaction Details
        [HttpGet]
        [Route("gettransactiondetails/{transaction_id}")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetTransactionDetails(int transaction_id)
        {
            if (!dbobj.IsTransactionOwnedByCustomerDB(transaction_id, GetCustomerId()))
            {
                return Forbid();
            }

            var transaction = dbobj.GetTransactionDetailsDB(transaction_id);

            if (transaction.transaction_id == 0)
                return NotFound(new { message = "Transaction not found" });

            return Ok(transaction);
        }




        // GET: api/<TransactionsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TransactionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
