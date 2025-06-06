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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionTypeDto[]))]
        public IActionResult GetTransactionTypes()
        {
            var transactionTypes = dbContext.TransactionTypes.
                Select(t => t.ToDto()).
                ToList();

            return Ok(transactionTypes);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTransactionType(int id)
        { 
            var transactionType = dbContext.TransactionTypes.Find(id);

            if (transactionType == null) return NotFound($"Could not find transactionType with id {id}");

            return Ok(transactionType.ToDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateTransactionType(TransactionType transactionType)
        {
            dbContext.TransactionTypes.Add(transactionType);
            dbContext.SaveChanges();

            return Created();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTransactionType(TransactionType transactionType, int id)
        {
            var transactionTypeDb = dbContext.TransactionTypes.Find(id);

            if (transactionTypeDb == null) return NotFound($"Could not find transactionType with id {id}.");
            if (transactionTypeDb.Id != id) return BadRequest("transactionType id does not match putted Id.");

            transactionTypeDb.Type = transactionType.Type;

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
