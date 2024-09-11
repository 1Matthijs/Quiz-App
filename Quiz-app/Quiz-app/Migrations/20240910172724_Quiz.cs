using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_app.Migrations
{
    /// <inheritdoc />
    public partial class Quiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Questions",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Questions",
                newName: "QuestionId");
        }
    }
}
