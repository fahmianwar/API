using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RoleRelations3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRoleRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_T_AccountRoles",
                table: "TB_T_AccountRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_T_AccountRoles",
                table: "TB_T_AccountRoles",
                columns: new[] { "NIK", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_AccountRoles_RoleId",
                table: "TB_T_AccountRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_AccountRoles_TB_M_Roles_RoleId",
                table: "TB_T_AccountRoles",
                column: "RoleId",
                principalTable: "TB_M_Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_AccountRoles_TB_M_Roles_RoleId",
                table: "TB_T_AccountRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_T_AccountRoles",
                table: "TB_T_AccountRoles");

            migrationBuilder.DropIndex(
                name: "IX_TB_T_AccountRoles_RoleId",
                table: "TB_T_AccountRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_T_AccountRoles",
                table: "TB_T_AccountRoles",
                column: "NIK");

            migrationBuilder.CreateTable(
                name: "AccountRoleRole",
                columns: table => new
                {
                    AccountRoleNIK = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoleRole", x => new { x.AccountRoleNIK, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AccountRoleRole_TB_M_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_M_Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRoleRole_TB_T_AccountRoles_AccountRoleNIK",
                        column: x => x.AccountRoleNIK,
                        principalTable: "TB_T_AccountRoles",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoleRole_RoleId",
                table: "AccountRoleRole",
                column: "RoleId");
        }
    }
}
