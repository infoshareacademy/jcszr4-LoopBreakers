using Microsoft.EntityFrameworkCore.Migrations;

namespace LoopBreakers.DAL.Migrations
{
    public partial class countryCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "TransferReport",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "TransferReport");
        }
    }
}
