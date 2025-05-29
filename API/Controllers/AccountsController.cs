using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account[]))]
        public IActionResult GetAccounts()
        {
            List<Account> accounts = dbContext.Accounts.ToList();
            return Ok(accounts);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAccount(int id)
        {
            Account? account = dbContext.Accounts.Find(id);
            return (account != null) ? Ok(account) : NotFound($"Could not find account with id {id}");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateAccount(Account account)
        { 
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account); 
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateAccount(Account account, int id)
        { 
            Account? accountDb = dbContext.Accounts.Find(id);

            if (accountDb == null) return NotFound($"Could not find account with id {id}.");
            if (accountDb.Id != id) return BadRequest("Account Id does not match putted Id.");

            dbContext.Accounts.Entry(accountDb).CurrentValues.SetValues(account);
            dbContext.SaveChanges();

            return Ok(account);
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
