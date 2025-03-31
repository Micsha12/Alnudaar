using Alnudaar2.Server.Models;
using Microsoft.EntityFrameworkCore;
using Alnudaar2.Server.Data; // Add this line
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alnudaar2.Server.Services
{
    public interface IDeviceService
    {
        Task<Device> RegisterDeviceAsync(Device device);
        Task<List<Device>> GetDevicesByUserIdAsync(int userId);
        Task<Device> UpdateDeviceNameAsync(int deviceId, string newName);
        Task<bool> DeleteDeviceAsync(int deviceId);
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

        public async Task<List<Device>> GetDevicesByUserIdAsync(int userId)
        {
            return await _context.Devices.Where(d => d.UserID == userId).ToListAsync();
        }

        public async Task<Device> UpdateDeviceNameAsync(int deviceId, string newName)
        {
            var device = await _context.Devices.FindAsync(deviceId);
            if (device != null)
            {
                device.Name = newName;
                await _context.SaveChangesAsync();
            }
            return device;
        }

        public async Task<bool> DeleteDeviceAsync(int deviceId)
        {
            var device = await _context.Devices.FindAsync(deviceId);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}