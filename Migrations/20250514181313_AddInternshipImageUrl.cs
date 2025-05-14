using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Internship_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class AddInternshipImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Internships",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Internships");
        }
    }
}
