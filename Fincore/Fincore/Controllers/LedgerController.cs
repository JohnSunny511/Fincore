using Fincore.DBclasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Fincore.Controllers
{
    [Route("Ledger")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        LedgerDBclss dbobj = new LedgerDBclss();
        AccountDBclss accdb = new AccountDBclss();
        TransactionDBclss transdb = new TransactionDBclss();

        private int GetCustomerId()
        {
            return Convert.ToInt32(User.FindFirst("reg_id")?.Value);
        }



        // Get Account Ledger
        [HttpGet]
        [Route("getaccountledger/{account_id}")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetAccountLedger(int account_id)
        {
            if (!accdb.IsAccountOwnedByCustomerDB(account_id, GetCustomerId()))
            {
                return Forbid();
            }

            var ledger = dbobj.GetAccountLedgerDB(account_id);

            if (ledger.Count == 0)
                return NotFound(new { message = "No ledger entries found for this account" });

            return Ok(ledger);
        }

        // Get Transaction Ledger
        [HttpGet]
        [Route("gettransactionledger/{transaction_id}")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetTransactionLedger(int transaction_id)
        {
            if (!transdb.IsTransactionOwnedByCustomerDB(transaction_id, GetCustomerId()))
            {
                return Forbid();
            }

            var ledger = dbobj.GetTransactionLedgerDB(transaction_id);

            if (ledger.Count == 0)
                return NotFound(new { message = "No ledger entries found for this transaction" });

            return Ok(ledger);
        }
    }
}