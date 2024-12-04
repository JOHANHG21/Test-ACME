using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_ACME.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSurveyFieldWithResponses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponses_SurveyFields_FieldId",
                table: "SurveyResponses");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponses_SurveyFields_FieldId",
                table: "SurveyResponses",
                column: "FieldId",
                principalTable: "SurveyFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponses_SurveyFields_FieldId",
                table: "SurveyResponses");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponses_SurveyFields_FieldId",
                table: "SurveyResponses",
                column: "FieldId",
                principalTable: "SurveyFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
