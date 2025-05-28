using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data.Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public ApplicationContext dbContext = new();

        [HttpGet]
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
            return (account != null) ? Ok(account) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateAccount(Account account)
        {
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account); 
        }
    }
}
