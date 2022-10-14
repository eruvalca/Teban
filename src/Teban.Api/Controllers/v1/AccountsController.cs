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
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _context.Accounts
                .Where(a => a.UserId == HttpContext.GetUserId())
                .ToListAsync();

            if (accounts is null)
            {
                return NotFound();
            }

            var successResponse = RequestResponseDto<IEnumerable<Account>>.Success(accounts);
            return Ok(successResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account is null)
            {
                var errorResponse = RequestResponseDto<Account>.Failure(new string[] { "The requested account does not exist." });
                return NotFound(errorResponse);
            }

            if (account.UserId != HttpContext.GetUserId())
            {
                var errorResponse = RequestResponseDto<Account>.Failure(new string[] { "The account does not belong to the logged in user." });
                return BadRequest(errorResponse);
            }

            var resultSet = await _context.Accounts
                .Where(a => a.UserId == HttpContext.GetUserId()
                    && a.AccountId == id)
                .Include(a => a.AccountTransactions)
                    .ThenInclude(at => at.TransactionEntries)
                .ToListAsync();

            var successResponse = RequestResponseDto<Account>.Success(resultSet.First());
            return Ok(successResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] Account account)
        {
            _context.Accounts.Add(account);
            var createResult = await _context.SaveChangesAsync();

            if (createResult < 1)
            {
                var errorResponse = RequestResponseDto<Account>.Failure(new string[] { "There was a problem creating the account." });
                return BadRequest(errorResponse);
            }

            var successResponse = RequestResponseDto<Account>.Success(account);
            return CreatedAtAction("GetAccount", new { id = account.AccountId }, successResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, [FromBody] Account account)
        {
            if (id != account.AccountId)
            {
                return BadRequest("The provided id does not match the id of the account.");
            }

            var existingAccount = await _context.Accounts.FindAsync(id);

            if (existingAccount is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested account does not exist." });
                return NotFound(errorResponse);
            }

            if (existingAccount.UserId != HttpContext.GetUserId())
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The account does not belong to the logged in user." });
                return BadRequest(errorResponse);
            }

            existingAccount.Name = account.Name;
            existingAccount.StartingBalance = account.StartingBalance;
            existingAccount.AccountType = account.AccountType;

            _context.Entry(existingAccount).State = EntityState.Modified;

            var updateResult = await _context.SaveChangesAsync();

            if (updateResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem updating the account." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested account does not exist." });
                return NotFound(errorResponse);
            }

            if (account.UserId != HttpContext.GetUserId())
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The account does not belong to the logged in user." });
                return BadRequest(errorResponse);
            }

            _context.Accounts.Remove(account);
            var deleteResult = await _context.SaveChangesAsync();

            if (deleteResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem deleting the account." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }
    }
}
