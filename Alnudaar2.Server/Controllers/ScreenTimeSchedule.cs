using Microsoft.AspNetCore.Mvc;
using Alnudaar2.Server.Models;
using Alnudaar2.Server.Services;

namespace Alnudaar2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenTimeScheduleController : ControllerBase
    {
        private readonly IScreenTimeScheduleService _screenTimeScheduleService;

        public ScreenTimeScheduleController(IScreenTimeScheduleService screenTimeScheduleService)
        {
            _screenTimeScheduleService = screenTimeScheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreenTimeSchedule>>> GetScreenTimeSchedules()
        {
            var schedules = await _screenTimeScheduleService.GetScreenTimeSchedulesAsync();
            return Ok(schedules);
        }

        [HttpPost]
        public async Task<ActionResult<ScreenTimeSchedule>> CreateScreenTimeSchedule(ScreenTimeSchedule schedule)
        {
            var createdSchedule = await _screenTimeScheduleService.CreateScreenTimeScheduleAsync(schedule);
            return CreatedAtAction(nameof(GetScreenTimeSchedules), new { id = createdSchedule.ScreenTimeScheduleID }, createdSchedule);
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