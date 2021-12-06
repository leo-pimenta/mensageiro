using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class CreateIndexSentAtMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"),
                column: "hashedpassword",
                value: "$2a$11$675quGSQRZtnDJhLtogGwOJXEWdDWprTNv4kC3jPjsY9qsS0OmC5u");

            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"),
                column: "hashedpassword",
                value: "$2a$11$FrlvT8AKJjC82AGcZrJl2uPgVaATraCAim8HkOBlp7meYzbeuQe/m");

            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"),
                column: "hashedpassword",
                value: "$2a$11$4HK5wD.L0bx.2OwFLsMW.emEAe3Vcp16CfgZ8St1bSfUM7/dRw7nG");

            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"),
                column: "hashedpassword",
                value: "$2a$11$wsj4ub75F27dxq/DvoukNuX.3JlDCCeMRUwJD/gFaevJXkbylXt.K");

            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"),
                column: "hashedpassword",
                value: "$2a$11$r7ZcTQjeiK1LfYBtrHYB9O0FwVl5YRfWlejt4V5gYB2zRCFHhh5Sy");

            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"),
                column: "hashedpassword",
                value: "$2a$11$2Fb2JtLLijcU91q/bnoptOzny0aw6Hs7V07rfO/8RY0b2eJAGMxui");

            migrationBuilder.CreateIndex(
                name: "ix_messages_sentat",
                schema: "mensageiro",
                table: "messages",
                column: "sentat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_messages_sentat",
                schema: "mensageiro",
                table: "messages");

            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"),
                column: "hashedpassword",
                value: "$2a$11$bmVxN1KVXvI7MgOHRDLjaOvL0uawngN3bYHGZOMHxxXGEttdZJ2aK");

            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"),
                column: "hashedpassword",
                value: "$2a$11$6e/1aqxZ0L9X9L81fwqVNuhuPPUi2jlqTvDwQIL5qayJYqJtsGnwS");

            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"),
                column: "hashedpassword",
                value: "$2a$11$KibIK2dZ1J.RB39OzLCUSu8evdekURhwp9ipRqZ16szPd9Qum.xqu");

            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"),
                column: "hashedpassword",
                value: "$2a$11$ar/L63qFY8IgiHTuxoaE7uU7I58.XNd5xUPD17TzC3aC2Yi14KcFm");

            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"),
                column: "hashedpassword",
                value: "$2a$11$jP6g4oa9Ixf0kniemoAGfOcJWIV1YEYYgJ7g7R/9T5t4S8W2yJcpy");

            migrationBuilder.UpdateData(
                schema: "mensageiro",
                table: "useraccounts",
                keyColumn: "userid",
                keyValue: new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"),
                column: "hashedpassword",
                value: "$2a$11$0/KIFBtYZyn5tJmAsb8ZQerC/WOBpfrX1YrM0l7SeOBUUGX/GKK0q");
        }
    }
}
