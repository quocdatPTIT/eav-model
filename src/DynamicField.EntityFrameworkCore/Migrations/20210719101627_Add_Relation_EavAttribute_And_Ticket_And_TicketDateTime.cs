using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicField.Migrations
{
    public partial class Add_Relation_EavAttribute_And_Ticket_And_TicketDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttributeId",
                table: "TicketDateTimeValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "TicketDateTimeValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketDateTimeValues_AttributeId",
                table: "TicketDateTimeValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDateTimeValues_TicketId",
                table: "TicketDateTimeValues",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDateTimeValues_EavAttributes_AttributeId",
                table: "TicketDateTimeValues",
                column: "AttributeId",
                principalTable: "EavAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDateTimeValues_Tickets_TicketId",
                table: "TicketDateTimeValues",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDateTimeValues_EavAttributes_AttributeId",
                table: "TicketDateTimeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketDateTimeValues_Tickets_TicketId",
                table: "TicketDateTimeValues");

            migrationBuilder.DropIndex(
                name: "IX_TicketDateTimeValues_AttributeId",
                table: "TicketDateTimeValues");

            migrationBuilder.DropIndex(
                name: "IX_TicketDateTimeValues_TicketId",
                table: "TicketDateTimeValues");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "TicketDateTimeValues");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketDateTimeValues");
        }
    }
}
