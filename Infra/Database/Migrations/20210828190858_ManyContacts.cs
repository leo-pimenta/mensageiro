using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class ManyContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_contacts_contactuserguid",
                schema: "mensageiro",
                table: "contacts");

            migrationBuilder.DropIndex(
                name: "ix_contacts_userguid",
                schema: "mensageiro",
                table: "contacts");

            migrationBuilder.CreateIndex(
                name: "ix_contacts_contactuserguid",
                schema: "mensageiro",
                table: "contacts",
                column: "contactuserguid");

            migrationBuilder.CreateIndex(
                name: "ix_contacts_userguid",
                schema: "mensageiro",
                table: "contacts",
                column: "userguid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_contacts_contactuserguid",
                schema: "mensageiro",
                table: "contacts");

            migrationBuilder.DropIndex(
                name: "ix_contacts_userguid",
                schema: "mensageiro",
                table: "contacts");

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
        }
    }
}
