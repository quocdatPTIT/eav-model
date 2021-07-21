using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicField.Migrations
{
    public partial class Add_EavAttributeOptionValue_Table_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EavAttributeOption_EavAttributes_AttributeId",
                table: "EavAttributeOption");

            migrationBuilder.DropForeignKey(
                name: "FK_EavAttributeOptionValue_EavAttributeOption_AttributeOptionId",
                table: "EavAttributeOptionValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EavAttributeOptionValue",
                table: "EavAttributeOptionValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EavAttributeOption",
                table: "EavAttributeOption");

            migrationBuilder.RenameTable(
                name: "EavAttributeOptionValue",
                newName: "EavAttributeOptionValues");

            migrationBuilder.RenameTable(
                name: "EavAttributeOption",
                newName: "EavAttributeOptions");

            migrationBuilder.RenameIndex(
                name: "IX_EavAttributeOptionValue_AttributeOptionId",
                table: "EavAttributeOptionValues",
                newName: "IX_EavAttributeOptionValues_AttributeOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_EavAttributeOption_AttributeId",
                table: "EavAttributeOptions",
                newName: "IX_EavAttributeOptions_AttributeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EavAttributeOptionValues",
                table: "EavAttributeOptionValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EavAttributeOptions",
                table: "EavAttributeOptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EavAttributeOptions_EavAttributes_AttributeId",
                table: "EavAttributeOptions",
                column: "AttributeId",
                principalTable: "EavAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EavAttributeOptionValues_EavAttributeOptions_AttributeOptionId",
                table: "EavAttributeOptionValues",
                column: "AttributeOptionId",
                principalTable: "EavAttributeOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EavAttributeOptions_EavAttributes_AttributeId",
                table: "EavAttributeOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_EavAttributeOptionValues_EavAttributeOptions_AttributeOptionId",
                table: "EavAttributeOptionValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EavAttributeOptionValues",
                table: "EavAttributeOptionValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EavAttributeOptions",
                table: "EavAttributeOptions");

            migrationBuilder.RenameTable(
                name: "EavAttributeOptionValues",
                newName: "EavAttributeOptionValue");

            migrationBuilder.RenameTable(
                name: "EavAttributeOptions",
                newName: "EavAttributeOption");

            migrationBuilder.RenameIndex(
                name: "IX_EavAttributeOptionValues_AttributeOptionId",
                table: "EavAttributeOptionValue",
                newName: "IX_EavAttributeOptionValue_AttributeOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_EavAttributeOptions_AttributeId",
                table: "EavAttributeOption",
                newName: "IX_EavAttributeOption_AttributeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EavAttributeOptionValue",
                table: "EavAttributeOptionValue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EavAttributeOption",
                table: "EavAttributeOption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EavAttributeOption_EavAttributes_AttributeId",
                table: "EavAttributeOption",
                column: "AttributeId",
                principalTable: "EavAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EavAttributeOptionValue_EavAttributeOption_AttributeOptionId",
                table: "EavAttributeOptionValue",
                column: "AttributeOptionId",
                principalTable: "EavAttributeOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
