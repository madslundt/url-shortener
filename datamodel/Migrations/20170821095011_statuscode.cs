using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace datamodel.Migrations
{
    public partial class statuscode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorCode",
                table: "UrlErrors");

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "UrlErrors",
                type: "int4",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "UrlErrors");

            migrationBuilder.AddColumn<int>(
                name: "ErrorCode",
                table: "UrlErrors",
                nullable: false,
                defaultValue: 0);
        }
    }
}
