using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.Api.Migrations
{
    /// <inheritdoc />
    public partial class AlterFruitsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "class",
                table: "fruits",
                type: "text",
                nullable: false,
                defaultValue: ""
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "class",
                table: "fruits"
            );
        }
    }
}
