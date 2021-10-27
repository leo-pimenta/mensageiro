using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class AddTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "users",
                columns: new[] { "guid", "email", "nickname" },
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
                columns: new[] { "guid", "blockguid", "contactuserguid", "userguid" },
                values: new object[,]
                {
                    { new Guid("cacddfe6-b38d-4fc4-9dea-7c07afe694e0"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("f2aebdd5-cb77-4e77-bbf9-1819faba1af3"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("7352bbdc-6ec5-4ed9-81bc-27c2581fa644"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("e0146d01-6ca6-47ab-b1b0-3388124957fc"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("7eeaafc4-fdf8-4ab6-9e07-2e936a400351"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("ac1c7692-c1fd-4d99-aa5a-ce21de22ad46"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("b7b68cbc-dc6f-42d6-99bc-1d12448c3864"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("5a2e29e3-eb77-434d-855b-2ce871ac1e81"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("0abd69fd-607e-4799-a949-649b934c9580"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") },
                    { new Guid("9c693843-ce4f-4d95-a67e-31d90070c167"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("7f6c20f6-caba-4cea-ac81-3d4ddee33fae"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("4116e323-770d-4a3a-b8bf-c7668ce3b43e"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("507f93db-40f9-403e-aeda-79cb3c157e7f"), null, new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("180b54ed-86f5-4619-b5a9-553add5bea58"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("bcf6c1c4-5ebe-44aa-bab8-6c7abde2a56e"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("8cd2dad0-b82e-4c6d-b3cf-d49727e69f91"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("707b42ec-6cac-4d16-b70e-b278c7bc91c7"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("c0d8bdd2-27e5-48e2-bc9b-666340ae4eb1"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("7655b59b-33b0-48fd-8b30-5cffc16220ec"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("3ea5a3b1-4d1e-4432-bec5-e0894762021b"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("929db624-8d41-4345-adba-d3049fc199ad"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("ee0b3e2b-d497-4c2e-9cf4-a69b59462270"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("411bb938-2af9-45c1-815f-e8d8cc64f80f"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596") },
                    { new Guid("a5e1a9e4-8756-4edc-9930-3a2ea6d4ba98"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("f88f0d53-2999-43de-9fcb-a67d23858840"), null, new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("edfdc02a-8b3d-435b-8d68-93cc83d72fc1"), null, new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51") },
                    { new Guid("787bd985-a7eb-477b-899d-2f5bfd1628dc"), null, new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d") },
                    { new Guid("6ed47c78-6047-40cb-8d99-73c0213e74b1"), null, new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2") },
                    { new Guid("1ea6f2b8-b2f1-45be-aeda-ce17fd1bfd99"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5") },
                    { new Guid("be80e0de-3b2e-4b66-84fd-c7f21bc9758c"), null, new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162") }
                });

            migrationBuilder.InsertData(
                schema: "mensageiro",
                table: "useraccounts",
                columns: new[] { "userguid", "hashedpassword" },
                values: new object[,]
                {
                    { new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), "$2a$11$z5SbtX7GPNS5nFSY0bhWZ.P5pw35.MEAV3YUlmxejHXJBfndnEepu" },
                    { new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), "$2a$11$bCpQt3fslvmv1NZNRf0t5uWPc4litq5NkX8.8kLnFYLOJDIYWqWQW" },
                    { new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), "$2a$11$GTReISRdU5brf4lDhyKDfOKzopu3hivWeVzDS3f1.iihIPuS9qCvW" },
                    { new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), "$2a$11$hvSBox04vb3Z8B23PcV1q.oxhfzuIuJaXbI2L3X.Xcstgfi2u4oOa" },
                    { new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), "$2a$11$oRtqCxm3EJTTPasfK2fmi.x0tnWO7WuinI5huob/M3Z67WBEF9ky6" },
                    { new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), "$2a$11$sZieAJ7i.Cz.sWvlhe1uZupFJWGvaBGQw0q.kRKvs3i.8q16AQwBS" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("0abd69fd-607e-4799-a949-649b934c9580"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("180b54ed-86f5-4619-b5a9-553add5bea58"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("1ea6f2b8-b2f1-45be-aeda-ce17fd1bfd99"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("3ea5a3b1-4d1e-4432-bec5-e0894762021b"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("4116e323-770d-4a3a-b8bf-c7668ce3b43e"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("411bb938-2af9-45c1-815f-e8d8cc64f80f"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("507f93db-40f9-403e-aeda-79cb3c157e7f"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("5a2e29e3-eb77-434d-855b-2ce871ac1e81"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("6ed47c78-6047-40cb-8d99-73c0213e74b1"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("707b42ec-6cac-4d16-b70e-b278c7bc91c7"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("7352bbdc-6ec5-4ed9-81bc-27c2581fa644"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("7655b59b-33b0-48fd-8b30-5cffc16220ec"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("787bd985-a7eb-477b-899d-2f5bfd1628dc"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("7eeaafc4-fdf8-4ab6-9e07-2e936a400351"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("7f6c20f6-caba-4cea-ac81-3d4ddee33fae"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("8cd2dad0-b82e-4c6d-b3cf-d49727e69f91"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("929db624-8d41-4345-adba-d3049fc199ad"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("9c693843-ce4f-4d95-a67e-31d90070c167"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("a5e1a9e4-8756-4edc-9930-3a2ea6d4ba98"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("ac1c7692-c1fd-4d99-aa5a-ce21de22ad46"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("b7b68cbc-dc6f-42d6-99bc-1d12448c3864"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("bcf6c1c4-5ebe-44aa-bab8-6c7abde2a56e"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("be80e0de-3b2e-4b66-84fd-c7f21bc9758c"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("c0d8bdd2-27e5-48e2-bc9b-666340ae4eb1"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("cacddfe6-b38d-4fc4-9dea-7c07afe694e0"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("e0146d01-6ca6-47ab-b1b0-3388124957fc"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("edfdc02a-8b3d-435b-8d68-93cc83d72fc1"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("ee0b3e2b-d497-4c2e-9cf4-a69b59462270"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("f2aebdd5-cb77-4e77-bbf9-1819faba1af3"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "contacts",
                keyColumn: "guid",
                keyValue: new Guid("f88f0d53-2999-43de-9fcb-a67d23858840"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userguid",
                keyValue: new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userguid",
                keyValue: new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userguid",
                keyValue: new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userguid",
                keyValue: new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userguid",
                keyValue: new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userguid",
                keyValue: new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "users",
                keyColumn: "guid",
                keyValue: new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "users",
                keyColumn: "guid",
                keyValue: new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "users",
                keyColumn: "guid",
                keyValue: new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "users",
                keyColumn: "guid",
                keyValue: new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "users",
                keyColumn: "guid",
                keyValue: new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"));

            migrationBuilder.DeleteData(
                schema: "mensageiro",
                table: "users",
                keyColumn: "guid",
                keyValue: new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"));
        }
    }
}
