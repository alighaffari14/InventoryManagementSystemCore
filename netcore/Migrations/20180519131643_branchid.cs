using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class branchid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "branchId",
                table: "PurchaseOrder",
                maxLength: 38,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_branchId",
                table: "PurchaseOrder",
                column: "branchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Branch_branchId",
                table: "PurchaseOrder",
                column: "branchId",
                principalTable: "Branch",
                principalColumn: "branchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Branch_branchId",
                table: "PurchaseOrder");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrder_branchId",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "branchId",
                table: "PurchaseOrder");
        }
    }
}
