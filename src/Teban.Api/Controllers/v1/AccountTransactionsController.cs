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
    public class AccountTransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountTransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountTransaction(int id)
        {
            var accountTransaction = await _context.AccountTransactions.FindAsync(id);

            if (accountTransaction is null)
            {
                var errorResponse = RequestResponseDto<AccountTransaction>.Failure(new string[] { "The requested transaction does not exist." });
                return NotFound(errorResponse);
            }

            var resultSet = await _context.AccountTransactions
                .Where(a => a.AccountTransactionId == id)
                .Include(a => a.TransactionEntries)
                .ToListAsync();

            var successResponse = RequestResponseDto<AccountTransaction>.Success(resultSet.First());
            return Ok(successResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostAccountTransaction([FromBody] AccountTransaction accountTransaction)
        {
            _context.AccountTransactions.Add(accountTransaction);
            var createResult = await _context.SaveChangesAsync();

            if (createResult < 1)
            {
                var errorResponse = RequestResponseDto<AccountTransaction>.Failure(new string[] { "There was a problem creating the transaction." });
                return BadRequest(errorResponse);
            }

            var successResponse = RequestResponseDto<AccountTransaction>.Success(accountTransaction);
            return CreatedAtAction("GetAccountTransaction", new { id = accountTransaction.AccountTransactionId }, successResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountTransaction(int id, [FromBody] AccountTransaction accountTransaction)
        {
            if (id != accountTransaction.AccountTransactionId)
            {
                return BadRequest("The provided id does not match the id of the transaction.");
            }

            var existingAccountTransaction = await _context.AccountTransactions.FindAsync(id);

            if (existingAccountTransaction is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested transaction does not exist." });
                return NotFound(errorResponse);
            }

            existingAccountTransaction.TransactionDate = accountTransaction.TransactionDate;
            existingAccountTransaction.Description = accountTransaction.Description;
            existingAccountTransaction.Payee = accountTransaction.Payee;
            existingAccountTransaction.IsTransfer = accountTransaction.IsTransfer;
            existingAccountTransaction.IsInflow = accountTransaction.IsInflow;

            _context.Entry(existingAccountTransaction).State = EntityState.Modified;

            var updateResult = await _context.SaveChangesAsync();

            if (updateResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem updating the transaction." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountTransaction(int id)
        {
            var accountTransaction = await _context.AccountTransactions.FindAsync(id);

            if (accountTransaction is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested transaction does not exist." });
                return NotFound(errorResponse);
            }

            _context.AccountTransactions.Remove(accountTransaction);
            var deleteResult = await _context.SaveChangesAsync();

            if (deleteResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem deleting the transaction." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }
    }
}
