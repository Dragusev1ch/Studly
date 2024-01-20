using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studly.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Challenges_ParentChallengeId",
                table: "Challenges");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Challenges_ParentChallengeId",
                table: "Challenges",
                column: "ParentChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Challenges_ParentChallengeId",
                table: "Challenges");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Challenges_ParentChallengeId",
                table: "Challenges",
                column: "ParentChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
