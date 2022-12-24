using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorManagement.DBclient.Migrations
{
    /// <inheritdoc />
    public partial class Code : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Contracts_Partners_PartnerId",
            //    table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_PartnerId",
                table: "Contracts");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "PartnerTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ContractStatus",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartnerTypes_Code",
                table: "PartnerTypes",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractStatus_Code",
                table: "ContractStatus",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PartnerTypes_Code",
                table: "PartnerTypes");

            migrationBuilder.DropIndex(
                name: "IX_ContractStatus_Code",
                table: "ContractStatus");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "PartnerTypes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ContractStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PartnerId",
                table: "Contracts",
                column: "PartnerId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Contracts_Partners_PartnerId",
            //    table: "Contracts",
            //    column: "PartnerId",
            //    principalTable: "Partners",
            //    principalColumn: "Guid",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
