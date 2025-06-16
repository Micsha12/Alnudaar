using Alnudaar2.Server.Data;
using Alnudaar2.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Alnudaar2.Server.Services
{
    public interface IBlockRulesService
    {
        Task<List<BlockRule>> GetBlockRulesByUserIdAsync(int userId);
        Task<List<BlockRule>> GetBlockRulesByDeviceIdAsync(int deviceId);
        Task<BlockRule> CreateBlockRuleAsync(BlockRule rule);
        Task DeleteBlockRuleAsync(int id);
        Task<BlockRule> GetBlockRuleByIdAsync(int id);
        Task<BlockRule> UpdateBlockRuleAsync(BlockRule updatedRule);
    }

    public class BlockRulesService : IBlockRulesService
    {
        private readonly ApplicationDbContext _context;

        public BlockRulesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BlockRule>> GetBlockRulesByUserIdAsync(int userId)
        {
            return await _context.BlockRules.Where(r => r.UserID == userId).ToListAsync();
        }

        public async Task<List<BlockRule>> GetBlockRulesByDeviceIdAsync(int deviceId)
        {
            return await _context.BlockRules.Where(r => r.DeviceID == deviceId).ToListAsync();
        }

        public async Task<BlockRule> CreateBlockRuleAsync(BlockRule rule)
        {
            _context.BlockRules.Add(rule);
            await _context.SaveChangesAsync();
            return rule;
        }

        public async Task DeleteBlockRuleAsync(int id)
        {
            var rule = await _context.BlockRules.FindAsync(id);
            if (rule != null)
            {
                _context.BlockRules.Remove(rule);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<BlockRule> GetBlockRuleByIdAsync(int id)
        {
            var rule = await _context.BlockRules.FindAsync(id);
            if (rule == null)
            {
                throw new KeyNotFoundException($"BlockRule with ID {id} was not found.");
            }
            return rule;
        }
        public async Task<BlockRule> UpdateBlockRuleAsync(BlockRule updatedRule)
        {
            _context.BlockRules.Update(updatedRule);
            await _context.SaveChangesAsync();
            return updatedRule;
        }
    }
}