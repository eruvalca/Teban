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
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionEntriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionEntry(int id)
        {
            var transactionEntry = await _context.TransactionEntries.FindAsync(id);

            if (transactionEntry is null)
            {
                var errorResponse = RequestResponseDto<TransactionEntry>.Failure(new string[] { "The requested entry does not exist." });
                return NotFound(errorResponse);
            }

            var resultSet = await _context.TransactionEntries
                .Where(t => t.TransactionEntryId == id)
                .FirstAsync();

            var successResponse = RequestResponseDto<TransactionEntry>.Success(resultSet);
            return Ok(successResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostTransactionEntry([FromBody] TransactionEntry transactionEntry)
        {
            _context.TransactionEntries.Add(transactionEntry);
            var createResult = await _context.SaveChangesAsync();

            if (createResult < 1)
            {
                var errorResponse = RequestResponseDto<TransactionEntry>.Failure(new string[] { "There was a problem creating the entry." });
                return BadRequest(errorResponse);
            }

            var successResponse = RequestResponseDto<TransactionEntry>.Success(transactionEntry);
            return CreatedAtAction("GetTransactionEntry", new { id = transactionEntry.TransactionEntryId }, successResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionEntry(int id, [FromBody] TransactionEntry transactionEntry)
        {
            if (id != transactionEntry.TransactionEntryId)
            {
                return BadRequest("The provided id does not match the id of the entry.");
            }

            var existingTransactionEntry = await _context.TransactionEntries.FindAsync(id);

            if (existingTransactionEntry is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested entry does not exist." });
                return NotFound(errorResponse);
            }

            existingTransactionEntry.DebitAmount = transactionEntry.DebitAmount;
            existingTransactionEntry.CreditAmount = transactionEntry.CreditAmount;

            _context.Entry(existingTransactionEntry).State = EntityState.Modified;

            var updateResult = await _context.SaveChangesAsync();

            if (updateResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem updating the entry." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionEntry(int id)
        {
            var transactionEntry = await _context.TransactionEntries.FindAsync(id);

            if (transactionEntry is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested entry does not exist." });
                return NotFound(errorResponse);
            }

            _context.TransactionEntries.Remove(transactionEntry);
            var deleteResult = await _context.SaveChangesAsync();

            if (deleteResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem deleting the entry." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }
    }
}
