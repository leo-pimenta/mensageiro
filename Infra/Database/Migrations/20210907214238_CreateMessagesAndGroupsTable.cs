using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class CreateMessagesAndGroupsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chatgroups",
                schema: "mensageiro",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chatgroups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                schema: "mensageiro",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    groupid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "text", nullable: true),
                    sentat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.id);
                    table.ForeignKey(
                        name: "groupid",
                        column: x => x.groupid,
                        principalSchema: "mensageiro",
                        principalTable: "chatgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userid",
                        column: x => x.userid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usergrouprelationships",
                schema: "mensageiro",
                columns: table => new
                {
                    groupid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usergrouprelationships", x => new { x.groupid, x.userid });
                    table.ForeignKey(
                        name: "fk_usergrouprelationships_chatgroups_groupid",
                        column: x => x.groupid,
                        principalSchema: "mensageiro",
                        principalTable: "chatgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usergrouprelationships_users_userid",
                        column: x => x.userid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_messages_groupid",
                schema: "mensageiro",
                table: "messages",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_messages_userid",
                schema: "mensageiro",
                table: "messages",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_usergrouprelationships_groupid",
                schema: "mensageiro",
                table: "usergrouprelationships",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_usergrouprelationships_userid",
                schema: "mensageiro",
                table: "usergrouprelationships",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "usergrouprelationships",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "chatgroups",
                schema: "mensageiro");
        }
    }
}
