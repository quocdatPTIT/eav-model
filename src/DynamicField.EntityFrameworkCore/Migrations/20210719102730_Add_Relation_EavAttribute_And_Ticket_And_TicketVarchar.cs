using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicField.Migrations
{
    public partial class Add_Relation_EavAttribute_And_Ticket_And_TicketVarchar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttributeId",
                table: "TicketVarcharValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "TicketVarcharValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketVarcharValues_AttributeId",
                table: "TicketVarcharValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketVarcharValues_TicketId",
                table: "TicketVarcharValues",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketVarcharValues_EavAttributes_AttributeId",
                table: "TicketVarcharValues",
                column: "AttributeId",
                principalTable: "EavAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketVarcharValues_Tickets_TicketId",
                table: "TicketVarcharValues",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketVarcharValues_EavAttributes_AttributeId",
                table: "TicketVarcharValues");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketVarcharValues_Tickets_TicketId",
                table: "TicketVarcharValues");

            migrationBuilder.DropIndex(
                name: "IX_TicketVarcharValues_AttributeId",
                table: "TicketVarcharValues");

            migrationBuilder.DropIndex(
                name: "IX_TicketVarcharValues_TicketId",
                table: "TicketVarcharValues");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "TicketVarcharValues");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketVarcharValues");
        }
    }
}
