using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alnudaar2.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddBlockRuleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScreenTimeSchedules_Devices_DeviceID",
                table: "ScreenTimeSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceID",
                table: "ScreenTimeSchedules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "BlockRules",
                columns: table => new
                {
                    BlockRuleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    TimeRange = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockRules", x => x.BlockRuleID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenTimeSchedules_Devices_DeviceID",
                table: "ScreenTimeSchedules",
                column: "DeviceID",
                principalTable: "Devices",
                principalColumn: "DeviceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScreenTimeSchedules_Devices_DeviceID",
                table: "ScreenTimeSchedules");

            migrationBuilder.DropTable(
                name: "BlockRules");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceID",
                table: "ScreenTimeSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenTimeSchedules_Devices_DeviceID",
                table: "ScreenTimeSchedules",
                column: "DeviceID",
                principalTable: "Devices",
                principalColumn: "DeviceID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
