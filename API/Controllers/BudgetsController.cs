using Microsoft.EntityFrameworkCore;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BudgetDto[]))]
        public IActionResult GetBudgets()
        {
            var budgets = dbContext.Budgets.
                Include(b => b.TransactionType).
                Select(b => b.ToDto()).
                ToList();

            return Ok(budgets);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BudgetDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBudget(int id)
        {
            var budget = dbContext.Budgets.
                Where(b => b.Id == id).
                Include(b => b.TransactionType).
                SingleOrDefault(); 

            if (budget == null) return NotFound($"Could not find budget with id {id}");

            return Ok(budget.ToDto()); 
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateBudget(Budget budget)
        {
            dbContext.Budgets.Add(budget);
            dbContext.SaveChanges();

            return Created();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateBudget(Budget budget, int id)
        {
            var budgetDb = dbContext.Budgets.Find(id);

            if (budgetDb == null) return NotFound($"Could not find budget with id {id}.");
            if (budgetDb.Id != id) return BadRequest("budget id does not match putted Id.");

            budgetDb.TransactionTypeId = budget.TransactionTypeId;
            budgetDb.StartRange = budget.StartRange;
            budgetDb.EndRange = budget.EndRange;
            budgetDb.GoalAmount = budget.GoalAmount; 

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
