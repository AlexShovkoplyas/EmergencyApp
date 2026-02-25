using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmergencyApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToContactPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "settings",
                table: "ContactPersons",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "settings",
                table: "ContactPersons");
        }
    }
}
