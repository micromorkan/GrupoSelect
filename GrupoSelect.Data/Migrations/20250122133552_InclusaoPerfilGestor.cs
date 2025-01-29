using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrupoSelect.Data.Migrations
{
    /// <inheritdoc />
    public partial class InclusaoPerfilGestor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_GroupUser_GroupUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GroupUserId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "GestorFee",
                table: "TableType",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TableTypeGestor",
                table: "Proposal",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GestorFee",
                table: "TableType");

            migrationBuilder.DropColumn(
                name: "TableTypeGestor",
                table: "Proposal");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupUserId",
                table: "Users",
                column: "GroupUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_GroupUser_GroupUserId",
                table: "Users",
                column: "GroupUserId",
                principalTable: "GroupUser",
                principalColumn: "Id");
        }
    }
}
