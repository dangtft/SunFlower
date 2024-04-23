using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSunFlower.Migrations
{
    /// <inheritdoc />
    public partial class AddEamilSubscribeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authoritiess",
                columns: table => new
                {
                    ID_Authorize = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Authorize = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authoritiess", x => x.ID_Authorize);
                });

            migrationBuilder.CreateTable(
                name: "EmailSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSubscriptions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authoritiess");

            migrationBuilder.DropTable(
                name: "EmailSubscriptions");
        }
    }
}
