using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreApi.Data.Migrations
{
    public partial class Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(maxLength: 30, nullable: false),
                    Lastname = table.Column<string>(maxLength: 30, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    TodoStateId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todo_TodoState_TodoStateId",
                        column: x => x.TodoStateId,
                        principalTable: "TodoState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Todo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TodoState",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "Pending" });

            migrationBuilder.InsertData(
                table: "TodoState",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "Ongoing" });

            migrationBuilder.InsertData(
                table: "TodoState",
                columns: new[] { "Id", "Title" },
                values: new object[] { 3, "Done" });

            migrationBuilder.CreateIndex(
                name: "IX_Todo_TodoStateId",
                table: "Todo",
                column: "TodoStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Todo_UserId",
                table: "Todo",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todo");

            migrationBuilder.DropTable(
                name: "TodoState");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
