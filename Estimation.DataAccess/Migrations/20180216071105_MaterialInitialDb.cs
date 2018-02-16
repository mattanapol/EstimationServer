using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Estimation.DataAccess.Migrations
{
    public partial class MaterialInitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(nullable: false),
                    MaterialType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(nullable: false),
                    Fittings = table.Column<decimal>(nullable: false),
                    ListPrice = table.Column<decimal>(nullable: false),
                    MainMaterialId = table.Column<int>(nullable: false),
                    Manpower = table.Column<decimal>(nullable: false),
                    MaterialType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NetPrice = table.Column<decimal>(nullable: false),
                    OfferPrice = table.Column<decimal>(nullable: false),
                    Painting = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Supporting = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_MainMaterials_MainMaterialId",
                        column: x => x.MainMaterialId,
                        principalTable: "MainMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MainMaterialId",
                table: "Materials",
                column: "MainMaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "MainMaterials");
        }
    }
}
