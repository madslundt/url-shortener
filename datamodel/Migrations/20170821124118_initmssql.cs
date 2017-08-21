using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace datamodel.Migrations
{
    public partial class initmssql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlErrors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlErrors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RedirectUrl = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ShortId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrlRedirects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Redirected = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlRedirects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlRedirects_Urls_UrlId",
                        column: x => x.UrlId,
                        principalTable: "Urls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlRedirects_UrlId",
                table: "UrlRedirects",
                column: "UrlId");

            migrationBuilder.CreateIndex(
                name: "IX_Urls_Id",
                table: "Urls",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Urls_RedirectUrl",
                table: "Urls",
                column: "RedirectUrl",
                unique: true,
                filter: "[RedirectUrl] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Urls_ShortId",
                table: "Urls",
                column: "ShortId",
                unique: true,
                filter: "[ShortId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlErrors");

            migrationBuilder.DropTable(
                name: "UrlRedirects");

            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
