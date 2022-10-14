using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teban.Api.Extensions;
using Teban.Application.Dtos.Budget;
using Teban.Application.Dtos.Request;
using Teban.Domain.Entities;
using Teban.Infrastructure.Persistence;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teban.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BudgetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBudgets()
        {
            var budgets = await _context.Budgets
                .Where(b => b.UserId == HttpContext.GetUserId())
                .ToListAsync();

            if (budgets is null)
            {
                return NotFound();
            }

            var successResponse = RequestResponseDto<IEnumerable<Budget>>.Success(budgets);
            return Ok(successResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudget(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);

            if (budget is null)
            {
                var errorResponse = RequestResponseDto<IEnumerable<Budget>>.Failure(new string[] { "The requested budget does not exist." });
                return NotFound(errorResponse);
            }

            if (budget.UserId != HttpContext.GetUserId())
            {
                var errorResponse = RequestResponseDto<IEnumerable<Budget>>.Failure(new string[] { "The budget does not belong to the logged in user." });
                return BadRequest(errorResponse);
            }

            var resultSet = await _context.Budgets
                .Where(b => b.UserId == HttpContext.GetUserId()
                    && b.BudgetId == id)
                .Include(b => b.Accounts)
                    .ThenInclude(a => a.AccountTransactions)
                        .ThenInclude(at => at.TransactionEntries)
                .Include(b => b.CategoryGroups)
                    .ThenInclude(c => c.Categories)
                .ToListAsync();

            var successResponse = RequestResponseDto<IEnumerable<Budget>>.Success(resultSet);
            return Ok(successResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostBudget([FromBody] Budget budget)
        {
            var newBudget = new Budget
            {
                Name = budget.Name,
                UserId = HttpContext.GetUserId(),
                CreatedBy = HttpContext.GetUserId()
            };

            _context.Budgets.Add(newBudget);
            var createResult = await _context.SaveChangesAsync();

            if (createResult < 1)
            {
                var errorResponse = RequestResponseDto<Budget>.Failure(new string[] { "There was a problem creating the budget." });
                return BadRequest(errorResponse);
            }

            var successResponse = RequestResponseDto<Budget>.Success(newBudget);
            return CreatedAtAction("GetBudget", new { id = newBudget.BudgetId }, successResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget(int id, [FromBody] Budget budget)
        {
            if (id != budget.BudgetId)
            {
                return BadRequest("The provided id does not matcht the id of the budget.");
            }

            var existingBudget = await _context.Budgets.FindAsync(id);

            if (existingBudget is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested budget does not exist." });
                return NotFound(errorResponse);
            }

            if (existingBudget.UserId != HttpContext.GetUserId())
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The budget does not belong to the logged in user." });
                return BadRequest(errorResponse);
            }

            existingBudget.Name = budget.Name;
            _context.Entry(existingBudget).State = EntityState.Modified;

            var updateResult = await _context.SaveChangesAsync();

            if (updateResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem updating the budget." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);

            if (budget is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested budget does not exist." });
                return NotFound(errorResponse);
            }

            if (budget.UserId != HttpContext.GetUserId())
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The budget does not belong to the logged in user." });
                return BadRequest(errorResponse);
            }

            _context.Budgets.Remove(budget);
            var deleteResult = await _context.SaveChangesAsync();

            if (deleteResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem deleting the budget." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }
    }
}
