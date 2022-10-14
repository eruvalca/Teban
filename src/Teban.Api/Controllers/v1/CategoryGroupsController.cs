using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teban.Api.Extensions;
using Teban.Application.Dtos.Request;
using Teban.Domain.Entities;
using Teban.Infrastructure.Persistence;

namespace Teban.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryGroupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryGroup(int id)
        {
            var categoryGroup = await _context.CategoryGroups.FindAsync(id);

            if (categoryGroup is null)
            {
                var errorResponse = RequestResponseDto<CategoryGroup>.Failure(new string[] { "The requested category group does not exist." });
                return NotFound(errorResponse);
            }

            var resultSet = await _context.CategoryGroups
                .Where(c => c.CategoryGroupId == id)
                .Include(c => c.Categories)
                .ToListAsync();

            var successResponse = RequestResponseDto<CategoryGroup>.Success(resultSet.First());
            return Ok(successResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategoryGroup([FromBody] CategoryGroup categoryGroup)
        {
            _context.CategoryGroups.Add(categoryGroup);
            var createResult = await _context.SaveChangesAsync();

            if (createResult < 1)
            {
                var errorResponse = RequestResponseDto<CategoryGroup>.Failure(new string[] { "There was a problem creating the category group." });
                return BadRequest(errorResponse);
            }

            var successResponse = RequestResponseDto<CategoryGroup>.Success(categoryGroup);
            return CreatedAtAction("GetCategoryGroup", new { id = categoryGroup.CategoryGroupId }, successResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryGroup(int id, [FromBody] CategoryGroup categoryGroup)
        {
            if (id != categoryGroup.CategoryGroupId)
            {
                return BadRequest("The provided id does not match the id of the category group.");
            }

            var existingCategoryGroup = await _context.CategoryGroups.FindAsync(id);

            if (existingCategoryGroup is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested category group does not exist." });
                return NotFound(errorResponse);
            }

            existingCategoryGroup.Name = categoryGroup.Name;

            _context.Entry(existingCategoryGroup).State = EntityState.Modified;

            var updateResult = await _context.SaveChangesAsync();

            if (updateResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem updating the category group." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryGroup(int id)
        {
            var categoryGroup = await _context.CategoryGroups.FindAsync(id);

            if (categoryGroup is null)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "The requested categoryGroup does not exist." });
                return NotFound(errorResponse);
            }

            _context.CategoryGroups.Remove(categoryGroup);
            var deleteResult = await _context.SaveChangesAsync();

            if (deleteResult < 1)
            {
                var errorResponse = RequestResponseDto<int>.Failure(new string[] { "There was a problem deleting the category group." });
                return BadRequest(errorResponse);
            }

            return NoContent();
        }
    }
}
