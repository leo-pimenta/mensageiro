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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blocks", x => x.id);
                });

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
                name: "users",
                schema: "mensageiro",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    nickname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contactinvitations",
                schema: "mensageiro",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    inviteduserid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contactinvitations", x => x.id);
                    table.ForeignKey(
                        name: "inviteduserid",
                        column: x => x.inviteduserid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userid",
                        column: x => x.userid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                schema: "mensageiro",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    contactuserid = table.Column<Guid>(type: "uuid", nullable: false),
                    blockid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contacts", x => x.id);
                    table.ForeignKey(
                        name: "blockid",
                        column: x => x.blockid,
                        principalSchema: "mensageiro",
                        principalTable: "blocks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "contactuserid",
                        column: x => x.contactuserid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userid",
                        column: x => x.userid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "useraccounts",
                schema: "mensageiro",
                columns: table => new
                {
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    hashedpassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_useraccounts", x => x.userid);
                    table.ForeignKey(
                        name: "fk_useraccounts_users_userid",
                        column: x => x.userid,
                        principalSchema: "mensageiro",
                        principalTable: "users",
                        principalColumn: "id",
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "users",
                columns: new[] { "id", "email", "nickname" },
                values: new object[,]
                {
                    { new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), "joao.teste@teste.com", "João" },
                    { new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), "leo.teste@teste.com", "Leo" },
                    { new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), "mariana.teste@teste.com", "Mariana" },
                    { new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), "matheus.teste@teste.com", "Matheus" },
                    { new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), "claudia.teste@teste.com", "Claudia" },
                    { new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), "luisfelipe.teste@teste.com", "Luís Felipe" }
                });

            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "contacts",
                columns: new[] { "id", "blockid", "contactuserid", "userid" },
                values: new object[,]
                {
                    { new Guid("1382664e-aedf-49d5-bc1a-556bf74ad8d3"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("55e0c157-e2f3-406a-b77a-e83ccd86e9d5"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("e43ed8c0-cea1-434b-b0cc-42d532c57084"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("984a42af-c6e0-414e-a073-f1e4d88aef43"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("49726b60-5062-4de2-ba19-19470a9ac00a"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("5d65e9f1-2e32-4955-b635-fde4c7515bcc"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("bfe245e5-23b3-47a4-bb8a-796eff1cd0fa"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("3854f03e-428b-4dc6-9f9b-91bbef773db9"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("e5deb8f7-42ea-4ffa-aa25-9976775e1a4b"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("5379b302-37eb-4e50-8bf1-141cf1036635"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("99c14afe-2136-464a-86d5-85f440095668"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("07aa5961-2cf0-46d2-a20b-7080df89acb7"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("59a7de72-46aa-4f99-9053-f77998e6b939"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("28e1a968-9d36-4f62-9485-2d20f1705564"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("bf1f415f-74cd-4498-a6f6-bb0c80e30c19"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("9d058082-ea41-4765-8ed6-b4c1bcc64c70"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("9553d96b-7bef-4cd6-84c1-10e5928d5341"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("bad9815f-7db6-4015-9ef6-c7715ebf32e3"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("38fe32f1-1c20-4d91-a155-895e6f4f0971"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("0bcfbc8a-2291-4e21-82b7-62fd50abb2a4"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("8414b7f9-ee5e-4bd3-91f7-47e5cff2c04a"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("54f1be1a-8a48-43a9-b85b-18ece69b60cd"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("db1d2c39-3b89-4fa9-a8c9-92d95ab4adee"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("d9fb8ef8-a42d-49cf-8b70-88ec114128c2"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("1c52e1cf-b1fb-4e18-ba64-6fee631b533d"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("f49424f6-f6c1-4312-b1e2-05c1640655a9"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("02b51f69-e653-44ab-9bc2-7317f317637c"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("2ee00791-0420-487c-8538-bf7d0812b035"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("96fb00f2-151f-4202-b513-a2087049afba"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("c9ab6a0c-c70d-4bfb-91a9-47cbe7c2fded"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") }
                });

            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "useraccounts",
                columns: new[] { "userid", "hashedpassword" },
                values: new object[,]
                {
                    { new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), "$2a$11$aguYvvIy3CaQvgdVxGmCzOTrEG7T96gdGy/O0XAvHHTujDBcqUK9O" },
                    { new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), "$2a$11$vKuatSDBj4ksTG.dNlvRl.pwl0wIdSOsWUqZzG/3HtWVzasD4Xjt2" },
                    { new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), "$2a$11$YffUtD7SLXP7zAcu35UnOOmS0cdgD2pOJ.oJLmK0DSRnHuxfji0Kq" },
                    { new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), "$2a$11$uoYAwovQvV8Q3oXJvddd9.ZwoqRkhxjnDqCG7FVILMTGrTils85hK" },
                    { new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), "$2a$11$oVh4UfF53vtQWyX544CqceHl9xuBElD/Wrr08z5HYOhYclnfcINky" },
                    { new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), "$2a$11$yMaGo6IAgVsM2RzCo2gfS.LPBn9NBZLbFZgwFtmJAD1A1rw4j2586" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_contactinvitations_inviteduserid",
                schema: "mensageiro",
                table: "contactinvitations",
                column: "inviteduserid");

            migrationBuilder.CreateIndex(
                name: "ix_contactinvitations_userid",
                schema: "mensageiro",
                table: "contactinvitations",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_contacts_blockid",
                schema: "mensageiro",
                table: "contacts",
                column: "blockid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_contacts_contactuserid",
                schema: "mensageiro",
                table: "contacts",
                column: "contactuserid");

            migrationBuilder.CreateIndex(
                name: "ix_contacts_userid",
                schema: "mensageiro",
                table: "contacts",
                column: "userid");

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
                name: "contactinvitations",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "contacts",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "messages",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "useraccounts",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "usergrouprelationships",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "blocks",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "chatgroups",
                schema: "mensageiro");

            migrationBuilder.DropTable(
                name: "users",
                schema: "mensageiro");
        }
    }
}
