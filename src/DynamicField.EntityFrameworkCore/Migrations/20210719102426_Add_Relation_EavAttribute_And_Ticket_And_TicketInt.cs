using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicField.Migrations
{
    public partial class Add_Relation_EavAttribute_And_Ticket_And_TicketInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttributeId",
                table: "TicketIntValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "TicketIntValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketIntValues_AttributeId",
                table: "TicketIntValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketIntValues_TicketId",
                table: "TicketIntValues",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketIntValues_EavAttributes_AttributeId",
                table: "TicketIntValues",
                column: "AttributeId",
                principalTable: "EavAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketIntValues_Tickets_TicketId",
                table: "TicketIntValues",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketIntValues_EavAttributes_AttributeId",
                table: "TicketIntValues");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketIntValues_Tickets_TicketId",
                table: "TicketIntValues");

            migrationBuilder.DropIndex(
                name: "IX_TicketIntValues_AttributeId",
                table: "TicketIntValues");

            migrationBuilder.DropIndex(
                name: "IX_TicketIntValues_TicketId",
                table: "TicketIntValues");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "TicketIntValues");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketIntValues");
        }
    }
}
