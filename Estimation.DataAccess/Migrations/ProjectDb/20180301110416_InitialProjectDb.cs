using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Estimation.DataAccess.Migrations.ProjectDb
{
    public partial class InitialProjectDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(nullable: false),
                    ConstructionPlace = table.Column<string>(nullable: true),
                    ConstructionScale = table.Column<string>(nullable: true),
                    ConstructionTerm = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CurrencyUnit = table.Column<string>(nullable: true),
                    GeneralContractor = table.Column<string>(nullable: true),
                    KindOfWork = table.Column<string>(nullable: true),
                    LabourCost = table.Column<decimal>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    MiscellaneousIsUsePercentage = table.Column<bool>(nullable: false),
                    MiscellaneousManual = table.Column<decimal>(nullable: false),
                    MiscellaneousPercentage = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Owner = table.Column<string>(nullable: true),
                    ProjectType = table.Column<string>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    SubmitBy = table.Column<string>(nullable: true),
                    TransportationIsUsePercentage = table.Column<bool>(nullable: false),
                    TransportationManual = table.Column<decimal>(nullable: false),
                    TransportationPercentage = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupCode = table.Column<string>(nullable: false),
                    GroupName = table.Column<string>(nullable: false),
                    ParentGroupId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialGroup_ProjectInfo_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<int>(nullable: false),
                    CodeAsString = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Fittings = table.Column<decimal>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    ListPrice = table.Column<decimal>(nullable: false),
                    Manpower = table.Column<decimal>(nullable: false),
                    MaterialGroupId = table.Column<int>(nullable: false),
                    MaterialType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    NetPrice = table.Column<decimal>(nullable: false),
                    OfferPrice = table.Column<decimal>(nullable: false),
                    Painting = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Supporting = table.Column<decimal>(nullable: false),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMaterials_MaterialGroup_MaterialGroupId",
                        column: x => x.MaterialGroupId,
                        principalTable: "MaterialGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialGroup_ProjectId",
                table: "MaterialGroup",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMaterials_MaterialGroupId",
                table: "ProjectMaterials",
                column: "MaterialGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectMaterials");

            migrationBuilder.DropTable(
                name: "MaterialGroup");

            migrationBuilder.DropTable(
                name: "ProjectInfo");
        }
    }
}
