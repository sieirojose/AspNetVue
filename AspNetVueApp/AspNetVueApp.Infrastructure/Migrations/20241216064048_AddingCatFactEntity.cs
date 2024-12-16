using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetVueApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingCatFactEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatFacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatFacts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatFacts");
        }
    }
}
