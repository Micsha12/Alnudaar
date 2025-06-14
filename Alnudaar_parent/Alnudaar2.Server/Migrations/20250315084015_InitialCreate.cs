using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alnudaar2.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    UserType = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceID);
                    table.ForeignKey(
                        name: "FK_Devices_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Geofences",
                columns: table => new
                {
                    GeofencingID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    SafeZoneName = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    Radius = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geofences", x => x.GeofencingID);
                    table.ForeignKey(
                        name: "FK_Geofences_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    ActivityLogID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    DeviceID = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Activity = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.ActivityLogID);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    AlertID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    DeviceID = table.Column<int>(type: "INTEGER", nullable: false),
                    AlertType = table.Column<string>(type: "TEXT", nullable: true),
                    AlertTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.AlertID);
                    table.ForeignKey(
                        name: "FK_Alerts_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alerts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsageReports",
                columns: table => new
                {
                    AppUsageReportID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    DeviceID = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AppName = table.Column<string>(type: "TEXT", nullable: true),
                    UsageDuration = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsageReports", x => x.AppUsageReportID);
                    table.ForeignKey(
                        name: "FK_AppUsageReports_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsageReports_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenTimeSchedules",
                columns: table => new
                {
                    ScreenTimeScheduleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    DeviceID = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<string>(type: "TEXT", nullable: true),
                    EndTime = table.Column<string>(type: "TEXT", nullable: true),
                    DayOfWeek = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenTimeSchedules", x => x.ScreenTimeScheduleID);
                    table.ForeignKey(
                        name: "FK_ScreenTimeSchedules_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScreenTimeSchedules_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_DeviceID",
                table: "ActivityLogs",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserID",
                table: "ActivityLogs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_DeviceID",
                table: "Alerts",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserID",
                table: "Alerts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsageReports_DeviceID",
                table: "AppUsageReports",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsageReports_UserID",
                table: "AppUsageReports",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserID",
                table: "Devices",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Geofences_UserID",
                table: "Geofences",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenTimeSchedules_DeviceID",
                table: "ScreenTimeSchedules",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenTimeSchedules_UserID",
                table: "ScreenTimeSchedules",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "AppUsageReports");

            migrationBuilder.DropTable(
                name: "Geofences");

            migrationBuilder.DropTable(
                name: "ScreenTimeSchedules");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
