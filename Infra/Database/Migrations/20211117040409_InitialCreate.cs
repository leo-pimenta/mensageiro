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
                    { new Guid("bbbb7699-0314-4e71-a903-00e52a48f6c0"), null },
                    { new Guid("c46d1c1c-14f8-4816-86e8-49edbd1c7aac"), null },
                    { new Guid("7ed7cb89-5460-47ea-bce8-8ba1e8403c0e"), null },
                    { new Guid("142ace51-f749-4856-b758-f0d21064cf6c"), null },
                    { new Guid("859fe3f9-37c6-445d-9de1-0fda6857ffd7"), null },
                    { new Guid("2f561e83-a3e0-4836-9980-e82ff6eed050"), null },
                    { new Guid("53f34079-fcfa-4bca-a9b8-e32739e7e1a0"), null },
                    { new Guid("0f8dfb7b-a625-47d0-a18f-346207c3dc65"), null },
                    { new Guid("64113c17-936b-41ac-ba47-b2f99dd6129e"), null },
                    { new Guid("04b053f0-7edc-4ee1-ab40-81a2ab4322e1"), null },
                    { new Guid("c210e1a1-3dc4-416a-852c-14df13d0e2b0"), null },
                    { new Guid("dd2b3ec1-0b44-4f69-a633-8961b9f8fdfc"), null },
                    { new Guid("860adda4-870d-4739-aa0d-e2964db4d407"), null },
                    { new Guid("bb54b14b-746c-4c33-b126-53daa1c9dcc2"), null },
                    { new Guid("cb1b4669-bd41-4457-97fd-e6bcaf567f6e"), null }
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
                columns: new[] { "id", "blockid", "contactuserid", "userid" },
                values: new object[,]
                {
                    { new Guid("05677bbe-3dea-422e-be4f-785d1446131e"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("6dd7ae39-a322-4870-ad18-ea3657f96498"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("063a4fdc-5d12-43ca-a3ac-2682017ef9ef"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("0b98c6c7-9240-4720-9c17-ffeeeb1d8cbd"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("713553d9-1122-42da-ba77-c0cc70bdc046"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("13a4b026-386f-4466-b42a-b55b423b7bae"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("3bf5e22f-68a5-4ea8-be44-e05d41965dfd"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("70fd25e5-116b-4c16-866b-36454de4c0a8"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("6cb2b131-4165-4234-9d21-5a80d725fc44"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("8b2e8c8b-d904-456e-beea-a3a3c9a5f393"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("7b7d3c1e-61bd-427c-b1d6-4bbcfe769337"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("e7f7770e-5ba8-441f-b1a7-c407559d37c4"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("088a19cb-146a-49e7-bf0a-74170873a92f"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("afdc515a-8f3a-4a49-b174-82d426dabd1b"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("c60c7299-bfb9-423a-8b6a-865ea1ca3b1a"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("1be8839a-94f1-4a82-affd-c1024c4c41e1"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("9ff79cb5-3e87-499a-abde-ca31d92038a0"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("e26badd2-66c8-49a1-84ec-9d600d1f6ce2"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("1af2a747-7d7d-47ad-9169-4e9d2e1e99f8"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("97abffd7-d6ea-4319-8f09-f0637667dccb"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("b552668f-d08d-4b82-8449-e9fd7b6402f6"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("bbf97855-f443-4419-8558-a6d0aeb57234"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("0055a6bb-c4e6-4c91-bdb4-bf62bd7ca07c"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("dbaa56c1-33fc-4f56-9ee7-29813168a060"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("02b73b52-d9d9-484b-b581-276caaa1df5f"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("bc4e0fa5-ca82-470b-8911-aacd54a1319d"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("6500b195-f35d-4266-b0e9-d09bf7a04800"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("9db1fa19-2a5e-4a4b-9f16-c5a28aec103e"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("80bbbf6d-2258-4142-ad9d-fa59b5f7d70c"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("8e05dae1-c4a8-451b-99b0-c62fde342a07"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") }
                });

            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "useraccounts",
                columns: new[] { "userid", "hashedpassword" },
                values: new object[,]
                {
                    { new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), "$2a$11$X1TE8FnbVerII5JDD2AxcOLltPXVzrXmpm8vUA6TKA0iM6bDtPV36" },
                    { new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), "$2a$11$pHuHQo1iPA54pYxPSnZm3OR9.rbshJFb4/p7/m1KpH3pZ8j7la2Zq" },
                    { new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), "$2a$11$YJlG0Pqw.2/zGnd0TOaeb.X3zMW03v8jfYDcVfrSNCiiC2p5UN9mS" },
                    { new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), "$2a$11$18kCD.jB6926HA8sAf5hneFE9gjYruQgqz6MIcchuERzSSyOoiJ/O" },
                    { new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), "$2a$11$9.6g62CHl1Pok6/H0cPZ5OxcirWrVtQhIofUHVnvRh2ULi8yrT8NS" },
                    { new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), "$2a$11$RvTc3V6tLGTwirz2XZ.9D.e.tX6a2O1MqiymlE2AGzbbpOl9anNvu" }
                });

            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "usergrouprelationships",
                columns: new[] { "groupid", "userid" },
                values: new object[,]
                {
                    { new Guid("dd2b3ec1-0b44-4f69-a633-8961b9f8fdfc"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("0f8dfb7b-a625-47d0-a18f-346207c3dc65"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("142ace51-f749-4856-b758-f0d21064cf6c"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("c46d1c1c-14f8-4816-86e8-49edbd1c7aac"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("c210e1a1-3dc4-416a-852c-14df13d0e2b0"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("53f34079-fcfa-4bca-a9b8-e32739e7e1a0"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("859fe3f9-37c6-445d-9de1-0fda6857ffd7"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("cb1b4669-bd41-4457-97fd-e6bcaf567f6e"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("64113c17-936b-41ac-ba47-b2f99dd6129e"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("142ace51-f749-4856-b758-f0d21064cf6c"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("bbbb7699-0314-4e71-a903-00e52a48f6c0"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("bb54b14b-746c-4c33-b126-53daa1c9dcc2"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("860adda4-870d-4739-aa0d-e2964db4d407"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("dd2b3ec1-0b44-4f69-a633-8961b9f8fdfc"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("c210e1a1-3dc4-416a-852c-14df13d0e2b0"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("bbbb7699-0314-4e71-a903-00e52a48f6c0"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("04b053f0-7edc-4ee1-ab40-81a2ab4322e1"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("64113c17-936b-41ac-ba47-b2f99dd6129e"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("7ed7cb89-5460-47ea-bce8-8ba1e8403c0e"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("cb1b4669-bd41-4457-97fd-e6bcaf567f6e"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("bb54b14b-746c-4c33-b126-53daa1c9dcc2"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("04b053f0-7edc-4ee1-ab40-81a2ab4322e1"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("2f561e83-a3e0-4836-9980-e82ff6eed050"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("0f8dfb7b-a625-47d0-a18f-346207c3dc65"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("859fe3f9-37c6-445d-9de1-0fda6857ffd7"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("860adda4-870d-4739-aa0d-e2964db4d407"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("7ed7cb89-5460-47ea-bce8-8ba1e8403c0e"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("2f561e83-a3e0-4836-9980-e82ff6eed050"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("53f34079-fcfa-4bca-a9b8-e32739e7e1a0"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("c46d1c1c-14f8-4816-86e8-49edbd1c7aac"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") }
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
