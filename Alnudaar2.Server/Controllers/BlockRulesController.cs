using Microsoft.AspNetCore.Mvc;
using Alnudaar2.Server.Models;
using Alnudaar2.Server.Services;

namespace Alnudaar2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockRulesController : ControllerBase
    {
        private readonly IBlockRulesService _blockRulesService;

        public BlockRulesController(IBlockRulesService blockRulesService)
        {
            _blockRulesService = blockRulesService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BlockRule>>> GetBlockRulesByUserId(int userId)
        {
            var rules = await _blockRulesService.GetBlockRulesByUserIdAsync(userId);
            return Ok(rules);
        }

        [HttpPost]
        public async Task<ActionResult<BlockRule>> CreateBlockRule(BlockRule rule)
        {
            var createdRule = await _blockRulesService.CreateBlockRuleAsync(rule);
            return CreatedAtAction(nameof(CreateBlockRule), new { id = createdRule.BlockRuleID }, createdRule);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlockRule(int id)
        {
            await _blockRulesService.DeleteBlockRuleAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BlockRule>> UpdateBlockRule(int id, BlockRule updatedRule)
        {
            if (id != updatedRule.BlockRuleID)
            {
                return BadRequest("Rule ID mismatch");
            }

            var existingRule = await _blockRulesService.GetBlockRuleByIdAsync(id);
            if (existingRule == null)
            {
                return NotFound();
            }

            // Ensure the DeviceID is valid (optional validation)
            if (updatedRule.DeviceID <= 0)
            {
                return BadRequest("Invalid DeviceID");
            }

            // Update the rule
            existingRule.Type = updatedRule.Type;
            existingRule.Value = updatedRule.Value;
            existingRule.TimeRange = updatedRule.TimeRange;
            existingRule.DeviceID = updatedRule.DeviceID;

            var updated = await _blockRulesService.UpdateBlockRuleAsync(existingRule);
            return Ok(updated);
        }
        
    }
}