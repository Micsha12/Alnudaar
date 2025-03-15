using Alnudaar2.Server.Models;
using Alnudaar2.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Alnudaar2.Server.Services
{
    public interface IScreenTimeScheduleService
    {
        Task<IEnumerable<ScreenTimeSchedule>> GetScreenTimeSchedulesAsync();
        Task<ScreenTimeSchedule> CreateScreenTimeScheduleAsync(ScreenTimeSchedule schedule);
        Task UpdateScreenTimeScheduleAsync(ScreenTimeSchedule schedule);
        Task DeleteScreenTimeScheduleAsync(int id);
    }

    public class ScreenTimeScheduleService : IScreenTimeScheduleService
    {
        private readonly ApplicationDbContext _context;

        public ScreenTimeScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ScreenTimeSchedule>> GetScreenTimeSchedulesAsync()
        {
            return await _context.ScreenTimeSchedules.ToListAsync();
        }

        public async Task<ScreenTimeSchedule> CreateScreenTimeScheduleAsync(ScreenTimeSchedule schedule)
        {
            _context.ScreenTimeSchedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task UpdateScreenTimeScheduleAsync(ScreenTimeSchedule schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteScreenTimeScheduleAsync(int id)
        {
            var schedule = await _context.ScreenTimeSchedules.FindAsync(id);
            if (schedule != null)
            {
                _context.ScreenTimeSchedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}