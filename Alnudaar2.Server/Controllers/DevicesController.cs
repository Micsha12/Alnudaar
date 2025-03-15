using Microsoft.AspNetCore.Mvc;
using Alnudaar2.Server.Models;
using Alnudaar2.Server.Services;

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
    }
}