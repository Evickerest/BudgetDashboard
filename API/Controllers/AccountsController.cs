using Microsoft.AspNetCore.Mvc;
using Data.Data;
using Data.Model;

namespace API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private ApplicationContext dbContext = new();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDto[]))]
        public IActionResult GetAccounts()
        {
            var accounts = dbContext.Accounts.
                Select(a => a.ToDto()).
                ToList();
 
            return Ok(accounts);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAccount(int id)
        { 
            var account = dbContext.Accounts.Find(id);

            if (account == null) return NotFound($"Could not find account with id {id}");

            return Ok(account.ToDto()); 
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateAccount(Account account)
        { 
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();

            return Created();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateAccount(Account account, int id)
        { 
            var accountDb = dbContext.Accounts.Find(id);

            if (accountDb == null) return NotFound($"Could not find account with id {id}.");
            if (accountDb.Id != id) return BadRequest("Account Id does not match putted Id.");

            accountDb.Name = account.Name;
            accountDb.Balance = account.Balance;
            accountDb.IsCredit = account.IsCredit;

            dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAccount(int id)
        {
            Account? account = dbContext.Accounts.Find(id);

            if (account == null) return NotFound($"Could not find account with id {id}");

            dbContext.Accounts.Remove(account);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
