using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicField.Migrations
{
    public partial class Add_Relation_EavAttribute_And_Ticket_TicketDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttributeId",
                table: "TicketDecimalValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "TicketDecimalValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketDecimalValues_AttributeId",
                table: "TicketDecimalValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDecimalValues_TicketId",
                table: "TicketDecimalValues",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDecimalValues_EavAttributes_AttributeId",
                table: "TicketDecimalValues",
                column: "AttributeId",
                principalTable: "EavAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDecimalValues_Tickets_TicketId",
                table: "TicketDecimalValues",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDecimalValues_EavAttributes_AttributeId",
                table: "TicketDecimalValues");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketDecimalValues_Tickets_TicketId",
                table: "TicketDecimalValues");

            migrationBuilder.DropIndex(
                name: "IX_TicketDecimalValues_AttributeId",
                table: "TicketDecimalValues");

            migrationBuilder.DropIndex(
                name: "IX_TicketDecimalValues_TicketId",
                table: "TicketDecimalValues");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "TicketDecimalValues");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketDecimalValues");
        }
    }
}
