using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teban.Application.Dtos.Request;
using Teban.Domain.Entities;
using Teban.Infrastructure.Persistence;

namespace Teban.Api.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
            {
                var errorResponse = RequestResponseDto<Category>.Failure(new string[] { "The requested category does not exist." });
                return NotFound(errorResponse);
            }

            var resultSet = await _context.Categories
                .Where(c => c.CategoryId == id)
                .FirstAsync();

            var successResponse = RequestResponseDto<Category>.Success(resultSet);
            return Ok(successResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] Category category)
        {
            _context.Categories.Add(category);
            var createResult = await _context.SaveChangesAsync();

            if (createResult < 1)
            {
                var errorResponse = RequestResponseDto<Category>.Failure(new string[] { "There was a problem creating the category." });
                return BadRequest(errorResponse);
            }

            var successResponse = RequestResponseDto<Category>.Success(category);
            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, successResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, [FromBody] Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("The provided id does not match the id of the category.");
            }

            var existingCategory = await _context.Categories.FindAsync(id);

            if (existingCategory is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested category does not exist." });
                return NotFound(errorResponse);
            }

            existingCategory.Name = category.Name;

            _context.Entry(existingCategory).State = EntityState.Modified;

            var updateResult = await _context.SaveChangesAsync();

            if (updateResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem updating the category." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested category does not exist." });
                return NotFound(errorResponse);
            }

            _context.Categories.Remove(category);
            var deleteResult = await _context.SaveChangesAsync();

            if (deleteResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem deleting the category." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }
    }
}
