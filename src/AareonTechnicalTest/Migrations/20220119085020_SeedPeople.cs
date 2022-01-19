using Microsoft.EntityFrameworkCore.Migrations;

namespace AareonTechnicalTest.Migrations
{
    public partial class SeedPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Forename", "IsAdmin", "Surname" },
                values: new object[] { 1, "System", true, "Admin" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Forename", "IsAdmin", "Surname" },
                values: new object[] { 2, "Test", false, "User" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PersonId",
                table: "Tickets",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Persons_PersonId",
                table: "Tickets",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Persons_PersonId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PersonId",
                table: "Tickets");

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
