using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class branchidcustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Vendor_vendorId",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_Customer_customerId",
                table: "SalesOrder");

            migrationBuilder.AlterColumn<string>(
                name: "customerId",
                table: "SalesOrder",
                maxLength: 38,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 38,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "branchId",
                table: "SalesOrder",
                maxLength: 38,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "vendorId",
                table: "PurchaseOrder",
                maxLength: 38,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 38,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_branchId",
                table: "SalesOrder",
                column: "branchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Vendor_vendorId",
                table: "PurchaseOrder",
                column: "vendorId",
                principalTable: "Vendor",
                principalColumn: "vendorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_Branch_branchId",
                table: "SalesOrder",
                column: "branchId",
                principalTable: "Branch",
                principalColumn: "branchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_Customer_customerId",
                table: "SalesOrder",
                column: "customerId",
                principalTable: "Customer",
                principalColumn: "customerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Vendor_vendorId",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_Branch_branchId",
                table: "SalesOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_Customer_customerId",
                table: "SalesOrder");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_branchId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "branchId",
                table: "SalesOrder");

            migrationBuilder.AlterColumn<string>(
                name: "customerId",
                table: "SalesOrder",
                maxLength: 38,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 38);

            migrationBuilder.AlterColumn<string>(
                name: "vendorId",
                table: "PurchaseOrder",
                maxLength: 38,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 38);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Vendor_vendorId",
                table: "PurchaseOrder",
                column: "vendorId",
                principalTable: "Vendor",
                principalColumn: "vendorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_Customer_customerId",
                table: "SalesOrder",
                column: "customerId",
                principalTable: "Customer",
                principalColumn: "customerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
