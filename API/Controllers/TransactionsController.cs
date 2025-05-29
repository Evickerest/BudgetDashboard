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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Transaction[]))]
        public IActionResult GetTransactions()
        {
            List<Transaction> transactions = dbContext.Transactions.ToList();
            return Ok(transactions);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Transaction))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTransaction(int id)
        {
            Transaction? transaction = dbContext.Transactions.Find(id);
            return (transaction != null) ? Ok(transaction) : NotFound($"Could not find transaction with id {id}");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateTransaction(Transaction transaction)
        { 
            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction); 
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Transaction))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTransaction(Transaction transaction, int id)
        { 
            Transaction? transactionDb = dbContext.Transactions.Find(id);

            if (transactionDb == null) return NotFound($"Could not find Transaction with id {id}.");
            if (transactionDb.Id != id) return BadRequest("transaction id does not match putted Id.");

            dbContext.Transactions.Entry(transactionDb).CurrentValues.SetValues(transaction);
            dbContext.SaveChanges();

            return Ok(transaction);
        }

        [HttpDelete]
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
