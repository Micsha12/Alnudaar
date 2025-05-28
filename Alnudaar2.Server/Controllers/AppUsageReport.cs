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
                var userExists = await _context.Users.AnyAsync(u => u.UserID == r.UserID);
                var deviceExists = await _context.Devices.AnyAsync(d => d.DeviceID == r.DeviceID);

                if (!userExists)
                    return BadRequest($"UserID {r.UserID} does not exist.");
                if (!deviceExists)
                    return BadRequest($"DeviceID {r.DeviceID} does not exist.");

                _context.AppUsageReports.Add(r);
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
    }
}