using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Jwt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_AccountRoles",
                columns: table => new
                {
                    NIK = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_AccountRoles", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_TB_T_AccountRoles_TB_M_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRoleRole");

            migrationBuilder.DropTable(
                name: "TB_M_Roles");

            migrationBuilder.DropTable(
                name: "TB_T_AccountRoles");
        }
    }
}
