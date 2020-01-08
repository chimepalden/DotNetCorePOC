using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetcorePoc.Migrations
{
    public partial class seedMemberTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 1, 3, "chime@gmail.com", "Chime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
