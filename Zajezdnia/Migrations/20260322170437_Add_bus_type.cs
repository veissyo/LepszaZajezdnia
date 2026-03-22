using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zajezdnia.Migrations
{
    /// <inheritdoc />
    public partial class Add_bus_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Typ",
                table: "Autobusy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Typ",
                table: "Autobusy");
        }
    }
}
