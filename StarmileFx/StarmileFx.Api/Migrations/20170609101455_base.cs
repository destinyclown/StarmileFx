using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarmileFx.Api.Migrations
{
    public partial class @base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysAuthorities",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Code = table.Column<string>(nullable: true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    PermissionsID = table.Column<int>(nullable: false),
                    State = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuthorities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SysEmailLogs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    EmailLog = table.Column<string>(nullable: true),
                    State = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysEmailLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SysLogs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Content = table.Column<string>(nullable: true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    ReceiveDate = table.Column<DateTime>(nullable: false),
                    Receiver = table.Column<int>(nullable: false),
                    SendDate = table.Column<DateTime>(nullable: false),
                    Sender = table.Column<int>(nullable: false),
                    State = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SysRoleLogs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    LoginIP = table.Column<string>(nullable: true),
                    RoleID = table.Column<int>(nullable: false),
                    State = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoleLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SysRolePermissions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    Explain = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Permissions = table.Column<int>(nullable: false),
                    State = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRolePermissions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SysRoles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    LoginName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Permissions = table.Column<int>(nullable: false),
                    Pwd = table.Column<string>(nullable: true),
                    State = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoles", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysAuthorities");

            migrationBuilder.DropTable(
                name: "SysEmailLogs");

            migrationBuilder.DropTable(
                name: "SysLogs");

            migrationBuilder.DropTable(
                name: "SysRoleLogs");

            migrationBuilder.DropTable(
                name: "SysRolePermissions");

            migrationBuilder.DropTable(
                name: "SysRoles");
        }
    }
}
