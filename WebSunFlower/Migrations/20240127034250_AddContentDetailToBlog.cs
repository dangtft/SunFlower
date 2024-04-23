using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSunFlower.Migrations
{
    /// <inheritdoc />
    public partial class AddContentDetailToBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentDetail",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentDetail",
                table: "Blogs");
        }
    }
}
