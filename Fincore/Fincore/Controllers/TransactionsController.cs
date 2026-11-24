using Fincore.DBclasses;
using Fincore.Models;
using Fincore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fincore.Controllers
{
    [Route("Transaction")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {

        TransactionDBclss dbobj = new TransactionDBclss();

        // Deposit
        [HttpPost]
        [Route("deposit")]
        public IActionResult Deposit(DepositDTO dto)
        {
            Transactionclss obj = new Transactionclss();

            obj.to_account_id = dto.account_id;
            obj.amount = dto.amount;
            obj.description = dto.description;

            int transaction_id = dbobj.DepositDB(obj);

            return Ok(new
            {
                message = "Deposit successful",
                transaction_id = transaction_id
            });
        }


        // Withdrawal
        [HttpPost]
        [Route("withdraw")]
        public IActionResult Withdraw(WithdrawalDTO dto)
        {
            Transactionclss obj = new Transactionclss();

            obj.from_account_id = dto.account_id;
            obj.amount = dto.amount;
            obj.description = dto.description;

            int transaction_id = dbobj.WithdrawDB(obj);

            return Ok(new
            {
                message = "Withdrawal successful",
                transaction_id = transaction_id
            });
        }


        // Transfer
        [HttpPost]
        [Route("transfer")]
        public IActionResult Transfer(TransferDTO dto)
        {
            Transactionclss obj = new Transactionclss();

            obj.from_account_id = dto.from_account_id;
            obj.to_account_id = dto.to_account_id;
            obj.amount = dto.amount;
            obj.description = dto.description;

            int transaction_id = dbobj.TransferDB(obj);

            return Ok(new
            {
                message = "Transfer successful",
                transaction_id = transaction_id
            });
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
