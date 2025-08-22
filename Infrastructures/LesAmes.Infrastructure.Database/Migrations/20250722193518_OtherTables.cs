using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LesAmes.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class OtherTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgeRangeId",
                table: "Souls",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreation",
                table: "Souls",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Souls",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Souls",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Souls",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quartier",
                table: "Souls",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sexe",
                table: "Souls",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Souls",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BirthYear",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgeRanges",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AgeMin = table.Column<int>(type: "integer", nullable: false),
                    AgeMax = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TutorId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgeRanges_AspNetUsers_TutorId",
                        column: x => x.TutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HobbyCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbyCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImpactFamilies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Quartiers = table.Column<string>(type: "text", nullable: true),
                    PilotName = table.Column<string>(type: "text", nullable: true),
                    PilotContact = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpactFamilies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    HobbyCategoryId = table.Column<string>(type: "text", nullable: false),
                    TutorId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hobbies_AspNetUsers_TutorId",
                        column: x => x.TutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hobbies_HobbyCategories_HobbyCategoryId",
                        column: x => x.HobbyCategoryId,
                        principalTable: "HobbyCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HobbySoul",
                columns: table => new
                {
                    HobbiesId = table.Column<string>(type: "text", nullable: false),
                    SoulsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbySoul", x => new { x.HobbiesId, x.SoulsId });
                    table.ForeignKey(
                        name: "FK_HobbySoul_Hobbies_HobbiesId",
                        column: x => x.HobbiesId,
                        principalTable: "Hobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HobbySoul_Souls_SoulsId",
                        column: x => x.SoulsId,
                        principalTable: "Souls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Souls_AgeRangeId",
                table: "Souls",
                column: "AgeRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgeRanges_TutorId",
                table: "AgeRanges",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_HobbyCategoryId",
                table: "Hobbies",
                column: "HobbyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_TutorId",
                table: "Hobbies",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_HobbySoul_SoulsId",
                table: "HobbySoul",
                column: "SoulsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Souls_AgeRanges_AgeRangeId",
                table: "Souls",
                column: "AgeRangeId",
                principalTable: "AgeRanges",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Souls_AgeRanges_AgeRangeId",
                table: "Souls");

            migrationBuilder.DropTable(
                name: "AgeRanges");

            migrationBuilder.DropTable(
                name: "HobbySoul");

            migrationBuilder.DropTable(
                name: "ImpactFamilies");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "HobbyCategories");

            migrationBuilder.DropIndex(
                name: "IX_Souls_AgeRangeId",
                table: "Souls");

            migrationBuilder.DropColumn(
                name: "AgeRangeId",
                table: "Souls");

            migrationBuilder.DropColumn(
                name: "DateCreation",
                table: "Souls");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Souls");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Souls");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Souls");

            migrationBuilder.DropColumn(
                name: "Quartier",
                table: "Souls");

            migrationBuilder.DropColumn(
                name: "Sexe",
                table: "Souls");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Souls");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "AspNetUsers");
        }
    }
}
