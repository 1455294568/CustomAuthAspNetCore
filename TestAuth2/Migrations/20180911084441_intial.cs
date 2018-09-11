using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestAuth2.Migrations
{
    public partial class intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(maxLength: 16, nullable: true),
                    passwd = table.Column<string>(maxLength: 16, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[]
                {
                    "username", "passwd", "CreationTime"
                },
                values: new object[] { "kevinlan", "kevinlan888", DateTime.Now }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
