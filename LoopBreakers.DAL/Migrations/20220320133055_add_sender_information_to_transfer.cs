using Microsoft.EntityFrameworkCore.Migrations;

namespace LoopBreakers.DAL.Migrations
{
    public partial class add_sender_information_to_transfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderFirstName",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderIban",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderLastName",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderFirstName",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "SenderIban",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "SenderLastName",
                table: "Transfers");
        }
    }
}
