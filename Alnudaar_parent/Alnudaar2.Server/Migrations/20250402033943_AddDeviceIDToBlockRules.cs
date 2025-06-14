using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alnudaar2.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceIDToBlockRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceID",
                table: "BlockRules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceID",
                table: "BlockRules");
        }
    }
}
