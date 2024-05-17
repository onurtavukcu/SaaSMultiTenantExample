using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WithMultiTenant.Migrations
{
    /// <inheritdoc />
    public partial class tenantDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubLevet",
                table: "Tenants",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubLevet",
                table: "Tenants");
        }
    }
}
