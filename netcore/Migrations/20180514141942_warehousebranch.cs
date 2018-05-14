using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class warehousebranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "branchId",
                table: "Warehouse",
                maxLength: 38,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_branchId",
                table: "Warehouse",
                column: "branchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Branch_branchId",
                table: "Warehouse",
                column: "branchId",
                principalTable: "Branch",
                principalColumn: "branchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Branch_branchId",
                table: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse_branchId",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "branchId",
                table: "Warehouse");
        }
    }
}
