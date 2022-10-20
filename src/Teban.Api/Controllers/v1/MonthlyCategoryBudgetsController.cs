using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teban.Api.Extensions;
using Teban.Application.Dtos.Request;
using Teban.Domain.Entities;
using Teban.Infrastructure.Persistence;

namespace Teban.Api.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MonthlyCategoryBudgetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MonthlyCategoryBudgetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonthlyCategoryBudget(int id)
        {
            var monthlyCategoryBudget = await _context.MonthlyCategoryBudgets.FindAsync(id);

            if (monthlyCategoryBudget is null)
            {
                var errorResponse = RequestResponseDto<MonthlyCategoryBudget>.Failure(new string[] { "The requested monthly category budget does not exist." });
                return NotFound(errorResponse);
            }

            var resultSet = await _context.MonthlyCategoryBudgets
                .Where(m => m.MonthlyCategoryBudgetId == id)
                .ToListAsync();

            var successResponse = RequestResponseDto<MonthlyCategoryBudget>.Success(resultSet.First());
            return Ok(successResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostMonthlyCategoryBudget([FromBody] MonthlyCategoryBudget monthlyCategoryBudget)
        {
            _context.MonthlyCategoryBudgets.Add(monthlyCategoryBudget);
            var createResult = await _context.SaveChangesAsync();

            if (createResult < 1)
            {
                var errorResponse = RequestResponseDto<MonthlyCategoryBudget>.Failure(new string[] { "There was a problem creating the monthly category budget." });
                return BadRequest(errorResponse);
            }

            var successResponse = RequestResponseDto<MonthlyCategoryBudget>.Success(monthlyCategoryBudget);
            return CreatedAtAction("GetMonthlyCategoryBudget", new { id = monthlyCategoryBudget.MonthlyCategoryBudgetId }, successResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonthlyCategoryBudget(int id, [FromBody] MonthlyCategoryBudget monthlyCategoryBudget)
        {
            if (id != monthlyCategoryBudget.MonthlyCategoryBudgetId)
            {
                return BadRequest("The provided id does not match the id of the monthly category budget.");
            }

            var existingMonthlyCategoryBudget = await _context.MonthlyCategoryBudgets.FindAsync(id);

            if (existingMonthlyCategoryBudget is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested monthly category budget does not exist." });
                return NotFound(errorResponse);
            }

            existingMonthlyCategoryBudget.Amount = monthlyCategoryBudget.Amount;
            _context.Entry(existingMonthlyCategoryBudget).State = EntityState.Modified;

            var updateResult = await _context.SaveChangesAsync();

            if (updateResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem updating the monthly category budget." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonthlyCategoryBudget(int id)
        {
            var monthlyCategoryBudget = await _context.MonthlyCategoryBudgets.FindAsync(id);

            if (monthlyCategoryBudget is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested monthly category budget does not exist." });
                return NotFound(errorResponse);
            }

            _context.MonthlyCategoryBudgets.Remove(monthlyCategoryBudget);
            var deleteResult = await _context.SaveChangesAsync();

            if (deleteResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem deleting the monthly category budget." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }
    }
}
