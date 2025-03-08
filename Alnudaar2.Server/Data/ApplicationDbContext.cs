using Microsoft.EntityFrameworkCore;
using Alnudaar2.Server.Models;

namespace Alnudaar2.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<AppUsageReport> AppUsageReports { get; set; }
        public DbSet<Geofencing> Geofences { get; set; }
        public DbSet<ScreenTimeSchedule> ScreenTimeSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.ActivityLogs)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Alerts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.AppUsageReports)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Geofences)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ScreenTimeSchedules)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Devices)
                .WithOne()
                .HasForeignKey(d => d.UserID);

            modelBuilder.Entity<Device>()
                .HasMany(d => d.ActivityLogs)
                .WithOne(a => a.Device)
                .HasForeignKey(a => a.DeviceID);

            modelBuilder.Entity<Device>()
                .HasMany(d => d.Alerts)
                .WithOne(a => a.Device)
                .HasForeignKey(a => a.DeviceID);

            modelBuilder.Entity<Device>()
                .HasMany(d => d.AppUsageReports)
                .WithOne(a => a.Device)
                .HasForeignKey(a => a.DeviceID);

            modelBuilder.Entity<Device>()
                .HasMany(d => d.ScreenTimeSchedules)
                .WithOne(s => s.Device)
                .HasForeignKey(s => s.DeviceID);
        }
    }
}