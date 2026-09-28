using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowDesk.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeAvailabilities_EmployeeId",
                table: "EmployeeAvailabilities");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAvailabilities_EmployeeId_DayOfWeek",
                table: "EmployeeAvailabilities",
                columns: new[] { "EmployeeId", "DayOfWeek" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeAvailabilities_EmployeeId_DayOfWeek",
                table: "EmployeeAvailabilities");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAvailabilities_EmployeeId",
                table: "EmployeeAvailabilities",
                column: "EmployeeId");
        }
    }
}
