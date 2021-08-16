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
                name: "blocks",
                schema: "mensageiro",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blocks", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "mensageiro",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    nickname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "contactinvitation",
                schema: "mensageiro",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    userguid = table.Column<Guid>(type: "uuid", nullable: false),
                    inviteduserguid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contactinvitation", x => x.guid);
                    table.ForeignKey(
                        name: "inviteduserguid",
                        column: x => x.inviteduserguid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userguid",
                        column: x => x.userguid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                schema: "mensageiro",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    userguid = table.Column<Guid>(type: "uuid", nullable: false),
                    contactuserguid = table.Column<Guid>(type: "uuid", nullable: false),
                    blockguid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contacts", x => x.guid);
                    table.ForeignKey(
                        name: "blockguid",
                        column: x => x.blockguid,
                        principalSchema: "mensageiro",
                        principalTable: "blocks",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "contactuserguid",
                        column: x => x.contactuserguid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userguid",
                        column: x => x.userguid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "useraccounts",
                schema: "mensageiro",
                columns: table => new
                {
                    userguid = table.Column<Guid>(type: "uuid", nullable: false),
                    hashedpassword = table.Column<string>(type: "text", nullable: false)
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
                name: "ix_contactinvitation_inviteduserguid",
                schema: "mensageiro",
                table: "contactinvitation",
                column: "inviteduserguid");

            migrationBuilder.CreateIndex(
                name: "ix_contactinvitation_userguid",
                schema: "mensageiro",
                table: "contactinvitation",
                column: "userguid");

            migrationBuilder.CreateIndex(
                name: "ix_contacts_blockguid",
                schema: "mensageiro",
                table: "contacts",
                column: "blockguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_contacts_contactuserguid",
                schema: "mensageiro",
                table: "contacts",
                column: "contactuserguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_contacts_userguid",
                schema: "mensageiro",
                table: "contacts",
                column: "userguid",
                unique: true);

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
                name: "contactinvitation",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "contacts",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "useraccounts",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "blocks",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "users",
                schema: "mensageiro");
        }
    }
}
