using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Data.Data;
using Data.Model;

namespace API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private ApplicationContext dbContext = new();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDto[]))]
        public IActionResult GetTransactions() 
        {
            var transactions = dbContext.Transactions.
                Include(t => t.TransactionType).
                Include(t => t.Account).
                Select(t => t.ToDto()).
                ToList();
                
            return Ok(transactions);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTransaction(int id)
        {
            var transaction = dbContext.Transactions.
                Where(t => t.Id == id).
                Include(t => t.TransactionType).
                Include(t => t.Account).
                SingleOrDefault();
            
            if (transaction == null) return NotFound($"Could not find transaction with id {id}");

            return Ok(transaction.ToDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateTransaction(Transaction transaction)
        { 
            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges();

            return Created();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTransaction(Transaction transaction, int id)
        { 
            var transactionDb = dbContext.Transactions.Find(id);

            if (transactionDb == null) return NotFound($"Could not find Transaction with id {id}.");
            if (transactionDb.Id != id) return BadRequest("transaction id does not match putted Id.");

            transactionDb.AccountId = transaction.AccountId;
            transactionDb.TransactionTypeId = transaction.TransactionTypeId;
            transactionDb.Value = transaction.Value;
            transactionDb.DateEntered = transaction.DateEntered;
            transactionDb.TransactionDate = transaction.TransactionDate;

            dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id:int}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTransaction(int id)
        {
            Transaction? transaction = dbContext.Transactions.Find(id);

            if (transaction == null) return NotFound($"Could not find transaction with id {id}");

            dbContext.Transactions.Remove(transaction);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
