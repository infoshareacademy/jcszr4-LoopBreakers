using Microsoft.EntityFrameworkCore.Migrations;

namespace LoopBreakers.DAL.Migrations
{
    public partial class add_fromId_field_to_recipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromId",
                table: "Recipients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Recipients");
        }
    }
}
