using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RelationshipTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_T_University",
                table: "TB_T_University");

            migrationBuilder.RenameTable(
                name: "TB_T_University",
                newName: "TB_M_University");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_University",
                table: "TB_M_University",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Profiling_EducationId",
                table: "TB_T_Profiling",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Education_UniversityId",
                table: "TB_M_Education",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Account_TB_T_Profiling_NIK",
                table: "TB_M_Account",
                column: "NIK",
                principalTable: "TB_T_Profiling",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Education_TB_M_University_UniversityId",
                table: "TB_M_Education",
                column: "UniversityId",
                principalTable: "TB_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Profiling_TB_M_Education_EducationId",
                table: "TB_T_Profiling",
                column: "EducationId",
                principalTable: "TB_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Account_TB_T_Profiling_NIK",
                table: "TB_M_Account");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Education_TB_M_University_UniversityId",
                table: "TB_M_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Profiling_TB_M_Education_EducationId",
                table: "TB_T_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_TB_T_Profiling_EducationId",
                table: "TB_T_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Education_UniversityId",
                table: "TB_M_Education");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_University",
                table: "TB_M_University");

            migrationBuilder.RenameTable(
                name: "TB_M_University",
                newName: "TB_T_University");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_T_University",
                table: "TB_T_University",
                column: "Id");
        }
    }
}
