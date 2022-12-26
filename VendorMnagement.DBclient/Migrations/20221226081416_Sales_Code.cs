using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorManagement.DBclient.Migrations
{
    /// <inheritdoc />
    public partial class SalesCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentNo",
                table: "Sales");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "DocumentNo",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
