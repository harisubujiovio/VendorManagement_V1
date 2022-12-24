using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorManagement.DBclient.Migrations
{
    /// <inheritdoc />
    public partial class UserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesGuid, x.UsersGuid });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesGuid",
                        column: x => x.RolesGuid,
                        principalTable: "Roles",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersGuid",
                        column: x => x.UsersGuid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersGuid",
                table: "RoleUser",
                column: "UsersGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");
        }
    }
}
