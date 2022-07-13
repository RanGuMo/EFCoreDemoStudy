using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreDemo.Api.Migrations
{
    public partial class updateUsers222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.EnsureSchema(
                name: "EFCoreDemo");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserList",
                newSchema: "EFCoreDemo");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "EFCoreDemo",
                table: "UserList",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "用户名",
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserList",
                schema: "EFCoreDemo",
                table: "UserList",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserList",
                schema: "EFCoreDemo",
                table: "UserList");

            migrationBuilder.RenameTable(
                name: "UserList",
                schema: "EFCoreDemo",
                newName: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "用户名");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
