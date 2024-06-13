using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Data.Migrations
{
    public partial class createbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    authors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "NVARCHAR(max)", nullable: false),
                    published_year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    average_rating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ratings_count = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    num_pages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    url_image = table.Column<string>(type: "NVARCHAR(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books",
                schema: "security");
        }
    }
}
