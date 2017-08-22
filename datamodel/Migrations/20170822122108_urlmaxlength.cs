using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace datamodel.Migrations
{
    public partial class urlmaxlength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Urls_RedirectUrl",
                table: "Urls");

            migrationBuilder.AlterColumn<string>(
                name: "RedirectUrl",
                table: "Urls",
                type: "VARCHAR(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RedirectUrl",
                table: "Urls",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(MAX)");

            migrationBuilder.CreateIndex(
                name: "IX_Urls_RedirectUrl",
                table: "Urls",
                column: "RedirectUrl",
                unique: true,
                filter: "[RedirectUrl] IS NOT NULL");
        }
    }
}
