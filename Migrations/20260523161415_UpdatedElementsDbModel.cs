using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crm.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedElementsDbModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "x_coordinate",
                table: "crm_elements");

            migrationBuilder.DropColumn(
                name: "y_coordinate",
                table: "crm_elements");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "crm_elements",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "crm_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "json",
                table: "crm_elements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "crm_elements");

            migrationBuilder.DropColumn(
                name: "json",
                table: "crm_elements");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "crm_elements",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "x_coordinate",
                table: "crm_elements",
                type: "integer",
                maxLength: 200,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "y_coordinate",
                table: "crm_elements",
                type: "integer",
                maxLength: 200,
                nullable: false,
                defaultValue: 0);
        }
    }
}
