﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExerciseTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserEntityCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ExerciseTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTasks_UserId",
                table: "ExerciseTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTasks_Users_UserId",
                table: "ExerciseTasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTasks_Users_UserId",
                table: "ExerciseTasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseTasks_UserId",
                table: "ExerciseTasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExerciseTasks");
        }
    }
}
