using Alnudaar2.Server.Models;
using Alnudaar2.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Alnudaar2.Server.Services
{
    public interface IDeviceService
    {
        Task<Device> RegisterDeviceAsync(Device device);
    }

    public class DeviceService : IDeviceService
    {
        private readonly ApplicationDbContext _context;

        public DeviceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Device> RegisterDeviceAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }
    }
}