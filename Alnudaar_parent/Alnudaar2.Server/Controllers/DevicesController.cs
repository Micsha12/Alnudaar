using Microsoft.AspNetCore.Mvc;
using Alnudaar2.Server.Models;
using Alnudaar2.Server.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alnudaar2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Device>> Register(Device device)
        {
            var createdDevice = await _deviceService.RegisterDeviceAsync(device);
            return CreatedAtAction(nameof(Register), new { id = createdDevice.DeviceID }, createdDevice);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Device>>> GetDevicesByUserId(int userId)
        {
            var devices = await _deviceService.GetDevicesByUserIdAsync(userId);
            return Ok(devices);
        }

        [HttpPut("{deviceId}")]
        public async Task<ActionResult<Device>> UpdateDeviceName(int deviceId, [FromBody] Device updatedDevice)
        {
            var device = await _deviceService.UpdateDeviceNameAsync(deviceId, updatedDevice.Name);
            if (device == null)
            {
                return NotFound();
            }
            return Ok(device);
        }

        [HttpDelete("{deviceId}")]
        public async Task<ActionResult> DeleteDevice(int deviceId)
        {
            var success = await _deviceService.DeleteDeviceAsync(deviceId);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("{deviceName}")]
        public async Task<ActionResult<Device>> GetDeviceByName(string deviceName)
        {
            var device = await _deviceService.GetDeviceByNameAsync(deviceName);
            if (device == null)
            {
                return NotFound(); // Return 404 if the device is not found
            }
            return Ok(device); // Return 200 with the device data
        }
    }
}