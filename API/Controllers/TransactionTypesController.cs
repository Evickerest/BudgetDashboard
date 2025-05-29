 using Microsoft.AspNetCore.Mvc;
using Data.Data;
using Data.Model;

namespace API.Controllers
{
    [Route("api/transaction-types")]
    [ApiController]
    public class TransactionTypesController : ControllerBase
    {
        private ApplicationContext dbContext = new();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionType[]))]
        public IActionResult GetTransactionTypes()
        {
            List<TransactionType> transactionTypes = dbContext.TransactionTypes.ToList();
            return Ok(transactionTypes);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTransactionType(int id)
        {
            TransactionType? transactionType = dbContext.TransactionTypes.Find(id);
            return (transactionType != null) ? Ok(transactionType) : NotFound($"Could not find transactionType with id {id}");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateTransactionType(TransactionType transactionType)
        {
            dbContext.TransactionTypes.Add(transactionType);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetTransactionType), new { id = transactionType.Id }, transactionType);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTransactionType(TransactionType transactionType, int id)
        {
            TransactionType? transactionTypeDb = dbContext.TransactionTypes.Find(id);

            if (transactionTypeDb == null) return NotFound($"Could not find transactionType with id {id}.");
            if (transactionTypeDb.Id != id) return BadRequest("transactionType id does not match putted Id.");

            dbContext.TransactionTypes.Entry(transactionTypeDb).CurrentValues.SetValues(transactionType);
            dbContext.SaveChanges();

            return Ok(transactionType);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTransactionType(int id)
        {
            TransactionType? transactionType = dbContext.TransactionTypes.Find(id);
            if (transactionType == null) return NotFound($"Could not find transactionType with id {id}");
            dbContext.TransactionTypes.Remove(transactionType);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
;
