using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrupoSelect.Data.Migrations
{
    /// <inheritdoc />
    public partial class CampoSellerProposal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Seller",
                table: "Proposal",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seller",
                table: "Proposal");
        }
    }
}
