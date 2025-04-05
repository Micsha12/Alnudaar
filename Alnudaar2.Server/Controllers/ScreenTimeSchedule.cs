using Microsoft.AspNetCore.Mvc;
using Alnudaar2.Server.Models;
using Alnudaar2.Server.Services;
using Alnudaar2.Server.Data; // Ensure this is the correct namespace for ApplicationDbContext
using Microsoft.EntityFrameworkCore;

namespace Alnudaar2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenTimeScheduleController : ControllerBase
    {
        private readonly IScreenTimeScheduleService _screenTimeScheduleService;
        private readonly ApplicationDbContext _context;
        public ScreenTimeScheduleController(IScreenTimeScheduleService screenTimeScheduleService, ApplicationDbContext context)
        {
            _screenTimeScheduleService = screenTimeScheduleService;
            _context = context;
        }
        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreenTimeSchedule>>> GetScreenTimeSchedules()
        {
            var schedules = await _screenTimeScheduleService.GetScreenTimeSchedulesAsync();
            return Ok(schedules);
        }

        [HttpGet("devices/{deviceId}")]
        public async Task<ActionResult<IEnumerable<ScreenTimeSchedule>>> GetScreenTimeSchedulesByDeviceId(int deviceId)
        {
            var schedules = await _screenTimeScheduleService.GetScreenTimeSchedulesByDeviceIdAsync(deviceId);
            if (schedules == null || schedules.Count == 0)
            {
                return NotFound($"No screen time schedules found for DeviceID: {deviceId}");
            }
            return Ok(schedules);
        }

        [HttpPost]
        public async Task<ActionResult<ScreenTimeSchedule>> CreateOrUpdateScreenTimeSchedule([FromBody] ScreenTimeSchedule schedule)
        {
            if (schedule == null || string.IsNullOrEmpty(schedule.DayOfWeek))
            {
                return BadRequest("Invalid schedule data.");
            }

            // Validate UserID
            var userExists = await _context.Users.AnyAsync(u => u.UserID == schedule.UserID);
            if (!userExists)
            {
                return BadRequest("Invalid UserID.");
            }

            // Validate DeviceID (if applicable)
            if (schedule.DeviceID.HasValue)
            {
                var deviceExists = await _context.Devices.AnyAsync(d => d.DeviceID == schedule.DeviceID);
                if (!deviceExists)
                {
                    return BadRequest("Invalid DeviceID.");
                }
            }

            try
            {
                // Check if a schedule for the given day already exists
                var existingSchedule = _context.ScreenTimeSchedules
                    .FirstOrDefault(s => s.DayOfWeek == schedule.DayOfWeek && s.UserID == schedule.UserID);

                if (existingSchedule != null)
                {
                    // Update the existing schedule
                    existingSchedule.StartTime = schedule.StartTime;
                    existingSchedule.EndTime = schedule.EndTime;
                    _context.ScreenTimeSchedules.Update(existingSchedule);
                }
                else
                {
                    // Create a new schedule
                    _context.ScreenTimeSchedules.Add(schedule);
                }

                await _context.SaveChangesAsync();
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScreenTimeSchedule(int id, ScreenTimeSchedule schedule)
        {
            if (id != schedule.ScreenTimeScheduleID)
            {
                return BadRequest();
            }

            await _screenTimeScheduleService.UpdateScreenTimeScheduleAsync(schedule);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreenTimeSchedule(int id)
        {
            await _screenTimeScheduleService.DeleteScreenTimeScheduleAsync(id);
            return NoContent();
        }
    }
}