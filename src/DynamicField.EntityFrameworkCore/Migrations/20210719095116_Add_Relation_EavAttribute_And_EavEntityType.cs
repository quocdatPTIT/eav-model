using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicField.Migrations
{
    public partial class Add_Relation_EavAttribute_And_EavEntityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EavEntityTypeId",
                table: "EavAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EavAttributes_EavEntityTypeId",
                table: "EavAttributes",
                column: "EavEntityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EavAttributes_EavEntityTypes_EavEntityTypeId",
                table: "EavAttributes",
                column: "EavEntityTypeId",
                principalTable: "EavEntityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EavAttributes_EavEntityTypes_EavEntityTypeId",
                table: "EavAttributes");

            migrationBuilder.DropIndex(
                name: "IX_EavAttributes_EavEntityTypeId",
                table: "EavAttributes");

            migrationBuilder.DropColumn(
                name: "EavEntityTypeId",
                table: "EavAttributes");
        }
    }
}
