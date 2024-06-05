using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KreatorQuizu.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Id_q = table.Column<int>(type: "INTEGER", nullable: false),
                    Answer_A = table.Column<string>(type: "TEXT", nullable: false),
                    Answer_B = table.Column<string>(type: "TEXT", nullable: false),
                    Answer_C = table.Column<string>(type: "TEXT", nullable: false),
                    Answer_D = table.Column<string>(type: "TEXT", nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
