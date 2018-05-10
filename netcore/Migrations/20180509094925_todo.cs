using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class todo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    todoId = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(maxLength: 100, nullable: true),
                    todoName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.todoId);
                });

            migrationBuilder.CreateTable(
                name: "TodoTask",
                columns: table => new
                {
                    todoTaskId = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(maxLength: 100, nullable: true),
                    todoId = table.Column<Guid>(nullable: false),
                    todoTaskName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoTask", x => x.todoTaskId);
                    table.ForeignKey(
                        name: "FK_TodoTask_Todo_todoId",
                        column: x => x.todoId,
                        principalTable: "Todo",
                        principalColumn: "todoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoTask_todoId",
                table: "TodoTask",
                column: "todoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoTask");

            migrationBuilder.DropTable(
                name: "Todo");
        }
    }
}
