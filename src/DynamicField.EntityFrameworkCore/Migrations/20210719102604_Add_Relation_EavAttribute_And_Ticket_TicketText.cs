using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicField.Migrations
{
    public partial class Add_Relation_EavAttribute_And_Ticket_TicketText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttributeId",
                table: "TicketTextValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "TicketTextValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTextValues_AttributeId",
                table: "TicketTextValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTextValues_TicketId",
                table: "TicketTextValues",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTextValues_EavAttributes_AttributeId",
                table: "TicketTextValues",
                column: "AttributeId",
                principalTable: "EavAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTextValues_Tickets_TicketId",
                table: "TicketTextValues",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTextValues_EavAttributes_AttributeId",
                table: "TicketTextValues");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTextValues_Tickets_TicketId",
                table: "TicketTextValues");

            migrationBuilder.DropIndex(
                name: "IX_TicketTextValues_AttributeId",
                table: "TicketTextValues");

            migrationBuilder.DropIndex(
                name: "IX_TicketTextValues_TicketId",
                table: "TicketTextValues");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "TicketTextValues");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketTextValues");
        }
    }
}
