using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mensageiro");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "mensageiro",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    nickname = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "useraccounts",
                schema: "mensageiro",
                columns: table => new
                {
                    userguid = table.Column<Guid>(type: "uuid", nullable: false),
                    hashedpassword = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_useraccounts", x => x.userguid);
                    table.ForeignKey(
                        name: "fk_useraccounts_users_userguid",
                        column: x => x.userguid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "mensageiro",
                table: "users",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "useraccounts",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "users",
                schema: "mensageiro");
        }
    }
}
