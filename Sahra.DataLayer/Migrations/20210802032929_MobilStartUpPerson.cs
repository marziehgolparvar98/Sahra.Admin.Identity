using Microsoft.EntityFrameworkCore.Migrations;

namespace Accelerator.DataLayer.Migrations
{
    public partial class MobilStartUpPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "RelationOfInvestReqs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "RelationOfInvestReqs");
        }
    }
}
