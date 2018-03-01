using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Estimation.DataAccess.Migrations
{
    public partial class InitialMaterialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    MaterialType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    MainMaterialId = table.Column<int>(nullable: false),
                    MaterialType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMaterials_MainMaterials_MainMaterialId",
                        column: x => x.MainMaterialId,
                        principalTable: "MainMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Fittings = table.Column<decimal>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    ListPrice = table.Column<decimal>(nullable: false),
                    Manpower = table.Column<decimal>(nullable: false),
                    MaterialType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    NetPrice = table.Column<decimal>(nullable: false),
                    OfferPrice = table.Column<decimal>(nullable: false),
                    Painting = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    SubMaterialId = table.Column<int>(nullable: false),
                    Supporting = table.Column<decimal>(nullable: false),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_SubMaterials_SubMaterialId",
                        column: x => x.SubMaterialId,
                        principalTable: "SubMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SubMaterialId",
                table: "Materials",
                column: "SubMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMaterials_MainMaterialId",
                table: "SubMaterials",
                column: "MainMaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "SubMaterials");

            migrationBuilder.DropTable(
                name: "MainMaterials");
        }
    }
}
