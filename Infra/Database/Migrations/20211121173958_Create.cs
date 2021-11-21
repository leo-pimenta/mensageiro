using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class Create : Migration
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
                    blockid = table.Column<Guid>(type: "uuid", nullable: true),
                    groupid = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "chatgroups",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("2b903f40-1f53-4c5b-beb3-b3cf5ac21ff3"), null },
                    { new Guid("8f525afe-0b26-4e29-8edb-ebaa62721a4e"), null },
                    { new Guid("a5836c08-6aa5-4d85-8c18-af8019cdec9f"), null },
                    { new Guid("7dc46b57-9f16-4857-9951-60b65e8d68ab"), null },
                    { new Guid("9ed9e0da-4c7e-4071-82ea-7f3617abde35"), null },
                    { new Guid("a498dcb0-3f6d-4f39-a354-9af1e18f3d9e"), null },
                    { new Guid("a125b75d-abce-42f6-b876-f18978312f4d"), null },
                    { new Guid("95bba9f6-c661-425f-9902-dcb08e00d949"), null },
                    { new Guid("167096b8-e066-46f0-a90e-446b540d331d"), null },
                    { new Guid("0d9bcff0-57ea-45ed-a566-3c829942e34b"), null },
                    { new Guid("ffb2fe92-612f-41c2-b77c-b9027e430be1"), null },
                    { new Guid("649a3553-5da3-4ce8-ad49-c51714bef758"), null },
                    { new Guid("04b8a481-221f-4bef-a5af-306204ced472"), null },
                    { new Guid("998f3c13-78f1-42cb-977d-dfa121ae3609"), null },
                    { new Guid("169e4880-3d26-4e5b-a14c-c1e214d17370"), null }
                });

            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "users",
                columns: new[] { "id", "email", "nickname" },
                values: new object[,]
                {
                    { new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), "claudia.teste@teste.com", "Claudia" },
                    { new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), "joao.teste@teste.com", "João" },
                    { new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), "leo.teste@teste.com", "Leo" },
                    { new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), "mariana.teste@teste.com", "Mariana" },
                    { new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), "matheus.teste@teste.com", "Matheus" },
                    { new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), "luisfelipe.teste@teste.com", "Luís Felipe" }
                });

            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "contacts",
                columns: new[] { "id", "blockid", "contactuserid", "groupid", "userid" },
                values: new object[,]
                {
                    { new Guid("e1f830b8-8974-44d4-b54b-8aa49ee03c5d"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("998f3c13-78f1-42cb-977d-dfa121ae3609"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("ae2f939b-8995-4e8c-8d84-ed59fd667d64"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("7dc46b57-9f16-4857-9951-60b65e8d68ab"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("ea263365-2265-49f4-acb6-1f4627ca5abe"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("649a3553-5da3-4ce8-ad49-c51714bef758"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("4d017a0e-10fc-4445-9211-63f59fe58dfe"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("04b8a481-221f-4bef-a5af-306204ced472"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("d674bd37-3f1c-4055-8e4f-265314ea729d"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("a498dcb0-3f6d-4f39-a354-9af1e18f3d9e"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("bd9e0c52-5ca1-4b6b-930d-b5b47992727b"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("167096b8-e066-46f0-a90e-446b540d331d"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("2e463f58-a3b9-4c81-84da-8450f8a91467"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("95bba9f6-c661-425f-9902-dcb08e00d949"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("71253f3e-484b-42ac-99f1-8348299b55ad"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("0d9bcff0-57ea-45ed-a566-3c829942e34b"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("906d3ae8-c93b-4710-a33c-9ac946a47931"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("a125b75d-abce-42f6-b876-f18978312f4d"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("5a12bb8f-6740-4c0d-86b8-4e8b1395234f"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("9ed9e0da-4c7e-4071-82ea-7f3617abde35"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("bdfb7c71-94f6-4eaf-b44a-2dfc9da95cb4"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("169e4880-3d26-4e5b-a14c-c1e214d17370"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("77d5673a-2b25-416a-854f-3c9fb537b914"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("8f525afe-0b26-4e29-8edb-ebaa62721a4e"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("3a7eadd1-c00c-470b-a18d-dfce31c28b01"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("2b903f40-1f53-4c5b-beb3-b3cf5ac21ff3"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("6286f15f-9d90-428f-9670-b6dd13110a4b"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("ffb2fe92-612f-41c2-b77c-b9027e430be1"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("c49b428f-1f4b-4870-b4e7-441052630305"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("a5836c08-6aa5-4d85-8c18-af8019cdec9f"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") }
                });

            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "useraccounts",
                columns: new[] { "userid", "hashedpassword" },
                values: new object[,]
                {
                    { new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), "$2a$11$fDU26b/yg9wha5ycDB5xaevpo/gePK/hxZEWj1U0.VyIaes6aPclG" },
                    { new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), "$2a$11$kCwbPSSCP.PFkBywyxvBjOfq5DSjOk338RwVL9TJ30os/8gk7StAO" },
                    { new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), "$2a$11$6g5SQJ.uXTQITRnHyoidiO4q8TXaIlHxFk5P4zw0Y1Xr42ZKQWvXW" },
                    { new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), "$2a$11$THjcJYGrYHnJPG1Alf45LuJrPb7FaD0DIzqHvFxFpLtGu2r3A2Uye" },
                    { new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), "$2a$11$Z7ackKoxmCcV/kUDlNKGE.hP1VzE.S9BP07pQti2F0CCywmxRM/iK" },
                    { new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), "$2a$11$Q2HYHdmUOXCrwDpiI52cv.ARAi6W5t8VnnLWd/0Iym9t7F5Ln.FQG" }
                });

            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "usergrouprelationships",
                columns: new[] { "groupid", "userid" },
                values: new object[,]
                {
                    { new Guid("95bba9f6-c661-425f-9902-dcb08e00d949"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("7dc46b57-9f16-4857-9951-60b65e8d68ab"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("ffb2fe92-612f-41c2-b77c-b9027e430be1"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("8f525afe-0b26-4e29-8edb-ebaa62721a4e"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("a125b75d-abce-42f6-b876-f18978312f4d"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("9ed9e0da-4c7e-4071-82ea-7f3617abde35"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("169e4880-3d26-4e5b-a14c-c1e214d17370"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("649a3553-5da3-4ce8-ad49-c51714bef758"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("04b8a481-221f-4bef-a5af-306204ced472"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("7dc46b57-9f16-4857-9951-60b65e8d68ab"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("2b903f40-1f53-4c5b-beb3-b3cf5ac21ff3"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("998f3c13-78f1-42cb-977d-dfa121ae3609"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("04b8a481-221f-4bef-a5af-306204ced472"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("649a3553-5da3-4ce8-ad49-c51714bef758"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("ffb2fe92-612f-41c2-b77c-b9027e430be1"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("2b903f40-1f53-4c5b-beb3-b3cf5ac21ff3"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("0d9bcff0-57ea-45ed-a566-3c829942e34b"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("167096b8-e066-46f0-a90e-446b540d331d"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("a5836c08-6aa5-4d85-8c18-af8019cdec9f"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("169e4880-3d26-4e5b-a14c-c1e214d17370"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("998f3c13-78f1-42cb-977d-dfa121ae3609"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("0d9bcff0-57ea-45ed-a566-3c829942e34b"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("a498dcb0-3f6d-4f39-a354-9af1e18f3d9e"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("95bba9f6-c661-425f-9902-dcb08e00d949"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("9ed9e0da-4c7e-4071-82ea-7f3617abde35"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("a5836c08-6aa5-4d85-8c18-af8019cdec9f"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("167096b8-e066-46f0-a90e-446b540d331d"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("a498dcb0-3f6d-4f39-a354-9af1e18f3d9e"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("a125b75d-abce-42f6-b876-f18978312f4d"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("8f525afe-0b26-4e29-8edb-ebaa62721a4e"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") }
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
                name: "ix_contacts_groupid",
                schema: "mensageiro",
                table: "contacts",
                column: "groupid",
                unique: true);

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
