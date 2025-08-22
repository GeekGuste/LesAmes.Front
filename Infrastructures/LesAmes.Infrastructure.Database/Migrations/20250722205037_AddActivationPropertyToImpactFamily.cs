﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LesAmes.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddActivationPropertyToImpactFamily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ImpactFamilies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ImpactFamilies");
        }
    }
}
