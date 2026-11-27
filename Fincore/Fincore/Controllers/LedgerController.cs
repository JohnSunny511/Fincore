using Fincore.DBclasses;
using Microsoft.AspNetCore.Mvc;

namespace Fincore.Controllers
{
    [Route("Ledger")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        LedgerDBclss dbobj = new LedgerDBclss();

        // Get Account Ledger
        [HttpGet]
        [Route("getaccountledger/{account_id}")]
        public IActionResult GetAccountLedger(int account_id)
        {
            var ledger = dbobj.GetAccountLedgerDB(account_id);

            if (ledger.Count == 0)
                return NotFound(new { message = "No ledger entries found for this account" });

            return Ok(ledger);
        }

        // Get Transaction Ledger
        [HttpGet]
        [Route("gettransactionledger/{transaction_id}")]
        public IActionResult GetTransactionLedger(int transaction_id)
        {
            var ledger = dbobj.GetTransactionLedgerDB(transaction_id);

            if (ledger.Count == 0)
                return NotFound(new { message = "No ledger entries found for this transaction" });

            return Ok(ledger);
        }
    }
}