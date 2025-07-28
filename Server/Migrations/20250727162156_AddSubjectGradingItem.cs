using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectGradingItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dummy",
                table: "SubjectGradingItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "dummy",
                table: "SubjectGradingItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
