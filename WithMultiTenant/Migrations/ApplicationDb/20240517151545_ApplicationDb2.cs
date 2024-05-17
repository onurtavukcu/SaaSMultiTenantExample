using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WithMultiTenant.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class ApplicationDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
