using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSunFlower.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthoritiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Authorities",
               columns: table => new
               {
                   ID_Authorize = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"), 
                   Authorize = table.Column<int>(nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Authorities", x => x.ID_Authorize);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authorities");
        }
    }
}
