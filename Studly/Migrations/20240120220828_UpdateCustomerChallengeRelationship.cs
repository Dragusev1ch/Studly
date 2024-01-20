using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studly.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerChallengeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Challenges_ChallengeId",
                table: "Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Customers_CustomerId",
                table: "Challenges");

            migrationBuilder.RenameColumn(
                name: "ChallengeId",
                table: "Challenges",
                newName: "ParentChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_Challenges_ChallengeId",
                table: "Challenges",
                newName: "IX_Challenges_ParentChallengeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Challenges",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "Challenges",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Challenges",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Challenges",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Challenges_ParentChallengeId",
                table: "Challenges",
                column: "ParentChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Customers_CustomerId",
                table: "Challenges",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Challenges_ParentChallengeId",
                table: "Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Customers_CustomerId",
                table: "Challenges");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Email",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "ParentChallengeId",
                table: "Challenges",
                newName: "ChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_Challenges_ParentChallengeId",
                table: "Challenges",
                newName: "IX_Challenges_ChallengeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Challenges",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Challenges",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Challenges",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Challenges",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Challenges_ChallengeId",
                table: "Challenges",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Customers_CustomerId",
                table: "Challenges",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }
    }
}
