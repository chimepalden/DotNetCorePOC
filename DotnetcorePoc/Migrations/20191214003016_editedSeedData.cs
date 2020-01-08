using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetcorePoc.Migrations
{
    public partial class editedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "palden@gmail.com", "Palden" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 2, 3, "chime@gmail.com", "Chime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "chime@gmail.com", "Chime" });
        }
    }
}
