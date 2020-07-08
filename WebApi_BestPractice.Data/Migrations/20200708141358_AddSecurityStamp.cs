using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi_BestPractice.Data.Migrations
{
    public partial class AddSecurityStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SecurityStamp",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Users");
        }
    }
}
