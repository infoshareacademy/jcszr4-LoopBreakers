using Microsoft.EntityFrameworkCore.Migrations;

namespace LoopBreakers.DAL.Migrations
{
    public partial class add_email_to_activity_report : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ActivityReport",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ActivityReport");
        }
    }
}
