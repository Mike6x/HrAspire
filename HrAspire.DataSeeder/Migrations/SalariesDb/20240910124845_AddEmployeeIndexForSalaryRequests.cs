﻿// <auto-generated/>
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrAspire.DataSeeder.Migrations.SalariesDb
{
    /// <inheritdoc />
    public partial class AddEmployeeIndexForSalaryRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SalaryRequests_EmployeeId",
                table: "SalaryRequests",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SalaryRequests_EmployeeId",
                table: "SalaryRequests");
        }
    }
}
