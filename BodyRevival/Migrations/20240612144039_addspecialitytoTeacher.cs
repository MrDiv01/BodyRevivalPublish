using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BodyRevival.Migrations
{
    /// <inheritdoc />
    public partial class addspecialitytoTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "Teacher",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "Teacher");
        }
    }
}
