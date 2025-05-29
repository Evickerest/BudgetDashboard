using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data.Data;
using Data.Model;

namespace API.Controllers
{
    [Route("api/budgets")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private ApplicationContext dbContext = new();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Budget[]))]
        public IActionResult GetBudgets()
        {
            List<Budget> budgets = dbContext.Budgets.ToList();
            return Ok(budgets);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Budget))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBudget(int id)
        {
            Budget? budget = dbContext.Budgets.Find(id);
            return (budget != null) ? Ok(budget) : NotFound($"Could not find budget with id {id}");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateBudget(Budget budget)
        {
            dbContext.Budgets.Add(budget);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetBudget), new { id = budget.Id }, budget);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Budget))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateBudget(Budget budget, int id)
        {
            Budget? budgetDb = dbContext.Budgets.Find(id);

            if (budgetDb == null) return NotFound($"Could not find budget with id {id}.");
            if (budgetDb.Id != id) return BadRequest("budget id does not match putted Id.");

            dbContext.Budgets.Entry(budgetDb).CurrentValues.SetValues(budget);
            dbContext.SaveChanges();

            return Ok(budget);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBudget(int id)
        {
            Budget? budget = dbContext.Budgets.Find(id);
            if (budget == null) return NotFound($"Could not find budget with id {id}");
            dbContext.Budgets.Remove(budget);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
;
