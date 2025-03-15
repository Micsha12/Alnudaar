using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alnudaar2.Server.Data;
using Alnudaar2.Server.Models;

namespace Computer_Control.Servieces
public class ScreenTimeService : ServiceBase
    {
        private readonly ApplicationDbContext _context;

        public ScreenTimeService()
        {
            _context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite("Data Source=../Alnudaar2_database/alnudaar_database.db")
                .Options);
        }

        protected override void OnStart(string[] args)
        {
            Task.Run(() => MonitorScreenTime());
        }

        private async Task MonitorScreenTime()
        {
            while (true)
            {
                var schedules = await _context.ScreenTimeSchedules.ToListAsync();
                foreach (var schedule in schedules)
                {
                    var now = DateTime.Now;
                    if (now.DayOfWeek.ToString() == schedule.DayOfWeek &&
                        now.TimeOfDay >= TimeSpan.Parse(schedule.StartTime) &&
                        now.TimeOfDay <= TimeSpan.Parse(schedule.EndTime))
                    {
                        // Enforce screen time limit (e.g., lock the device or log out the user)
                    }
                }
                await Task.Delay(60000); // Check every minute
            }
        }

        protected override void OnStop()
        {
            // Cleanup code here
        }
    }