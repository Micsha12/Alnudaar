﻿// <auto-generated />
using System;
using Alnudaar2.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Alnudaar2.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("Alnudaar2.Server.Models.ActivityLog", b =>
                {
                    b.Property<int>("ActivityLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Activity")
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ActivityLogID");

                    b.HasIndex("DeviceID");

                    b.HasIndex("UserID");

                    b.ToTable("ActivityLogs");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.Alert", b =>
                {
                    b.Property<int>("AlertID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AlertTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("AlertType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Details")
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AlertID");

                    b.HasIndex("DeviceID");

                    b.HasIndex("UserID");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.AppUsageReport", b =>
                {
                    b.Property<int>("AppUsageReportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AppName")
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("UsageDuration")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AppUsageReportID");

                    b.HasIndex("DeviceID");

                    b.HasIndex("UserID");

                    b.ToTable("AppUsageReports");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.BlockRule", b =>
                {
                    b.Property<int>("BlockRuleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeviceID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TimeRange")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BlockRuleID");

                    b.ToTable("BlockRules");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.Device", b =>
                {
                    b.Property<int>("DeviceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("DeviceID");

                    b.HasIndex("UserID");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.Geofencing", b =>
                {
                    b.Property<int>("GeofencingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<int>("Radius")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SafeZoneName")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("GeofencingID");

                    b.HasIndex("UserID");

                    b.ToTable("Geofences");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.ScreenTimeSchedule", b =>
                {
                    b.Property<int>("ScreenTimeScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DayOfWeek")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeviceID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ScreenTimeScheduleID");

                    b.HasIndex("DeviceID");

                    b.HasIndex("UserID");

                    b.ToTable("ScreenTimeSchedules");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.ActivityLog", b =>
                {
                    b.HasOne("Alnudaar2.Server.Models.Device", "Device")
                        .WithMany("ActivityLogs")
                        .HasForeignKey("DeviceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Alnudaar2.Server.Models.User", "User")
                        .WithMany("ActivityLogs")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.Alert", b =>
                {
                    b.HasOne("Alnudaar2.Server.Models.Device", "Device")
                        .WithMany("Alerts")
                        .HasForeignKey("DeviceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Alnudaar2.Server.Models.User", "User")
                        .WithMany("Alerts")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.AppUsageReport", b =>
                {
                    b.HasOne("Alnudaar2.Server.Models.Device", "Device")
                        .WithMany("AppUsageReports")
                        .HasForeignKey("DeviceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Alnudaar2.Server.Models.User", "User")
                        .WithMany("AppUsageReports")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.Device", b =>
                {
                    b.HasOne("Alnudaar2.Server.Models.User", "User")
                        .WithMany("Devices")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.Geofencing", b =>
                {
                    b.HasOne("Alnudaar2.Server.Models.User", "User")
                        .WithMany("Geofences")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.ScreenTimeSchedule", b =>
                {
                    b.HasOne("Alnudaar2.Server.Models.Device", "Device")
                        .WithMany("ScreenTimeSchedules")
                        .HasForeignKey("DeviceID");

                    b.HasOne("Alnudaar2.Server.Models.User", "User")
                        .WithMany("ScreenTimeSchedules")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.Device", b =>
                {
                    b.Navigation("ActivityLogs");

                    b.Navigation("Alerts");

                    b.Navigation("AppUsageReports");

                    b.Navigation("ScreenTimeSchedules");
                });

            modelBuilder.Entity("Alnudaar2.Server.Models.User", b =>
                {
                    b.Navigation("ActivityLogs");

                    b.Navigation("Alerts");

                    b.Navigation("AppUsageReports");

                    b.Navigation("Devices");

                    b.Navigation("Geofences");

                    b.Navigation("ScreenTimeSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
