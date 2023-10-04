using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employees.Data.Migrations
{
    public partial class UserActiondate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActionDate",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionDate",
                table: "Users");
        }
    }
}
