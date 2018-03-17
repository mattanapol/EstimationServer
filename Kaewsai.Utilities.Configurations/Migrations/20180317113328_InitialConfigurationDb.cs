using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Kaewsai.Utilities.Configurations.Migrations
{
    public partial class InitialConfigurationDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigurationDict",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationDict", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationEntries",
                columns: table => new
                {
                    ConfigurationEntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigurationDictTitle = table.Column<string>(nullable: false),
                    EnglishDescription = table.Column<string>(nullable: true),
                    InternalDescription = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    ThaiDescription = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationEntries", x => x.ConfigurationEntryId);
                    table.ForeignKey(
                        name: "FK_ConfigurationEntries_ConfigurationDict_ConfigurationDictTitle",
                        column: x => x.ConfigurationDictTitle,
                        principalTable: "ConfigurationDict",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationEntries_ConfigurationDictTitle",
                table: "ConfigurationEntries",
                column: "ConfigurationDictTitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationEntries");

            migrationBuilder.DropTable(
                name: "ConfigurationDict");
        }
    }
}
