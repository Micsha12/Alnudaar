using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alnudaar2.Server.Models;
using Alnudaar2.Server.Data;
using System.Threading.Tasks;

namespace Alnudaar2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsageReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppUsageReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
    public async Task<IActionResult> UploadAppUsageReport([FromBody] List<AppUsageReport> report)
    {
        if (report == null || report.Count == 0)
            return BadRequest("Invalid report data.");

        foreach (var r in report)
        {
            // Check for existing entry for the same user, device, app, and date
            var existing = await _context.AppUsageReports.FirstOrDefaultAsync(x =>
                x.UserID == r.UserID &&
                x.DeviceID == r.DeviceID &&
                x.AppName == r.AppName &&
                x.Timestamp.Date == r.Timestamp.Date);

            if (existing != null)
            {
                // Update the existing entry (e.g., sum durations)
                existing.UsageDuration += r.UsageDuration;
                _context.AppUsageReports.Update(existing);
            }
            else
            {
                _context.AppUsageReports.Add(r);
            }
        }

        await _context.SaveChangesAsync();
        return Ok(report);
    }

        [HttpGet("device/{deviceId}")]
        public async Task<ActionResult<IEnumerable<AppUsageReport>>> GetReportsByDevice(int deviceId)
        {
            var reports = await _context.AppUsageReports
                .Where(r => r.DeviceID == deviceId)
                .ToListAsync();

            // Always return a JSON array, even if empty
            return Ok(reports);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<AppUsageReport>>> GetReportsByUser(int userId)
        {
            var reports = await _context.AppUsageReports
                .Where(r => r.UserID == userId)
                .ToListAsync();

            if (reports == null || reports.Count == 0)
                return NotFound($"No reports found for user {userId}");

            return Ok(reports);
        }
        
    }
}