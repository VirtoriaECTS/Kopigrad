using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kopigrad.Migrations
{
    /// <inheritdoc />
    public partial class RenameAndAddColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Переименование столбца Email в Contact
            migrationBuilder.Sql("ALTER TABLE `Order` CHANGE COLUMN `Email` `Contact` VARCHAR(255) NOT NULL;");

            // Добавление нового столбца ContactType
            migrationBuilder.AddColumn<string>(
                name: "ContactType",
                table: "Order",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Переименовать обратно Contact в Email
            migrationBuilder.Sql("ALTER TABLE `Order` CHANGE COLUMN `Contact` `Email` VARCHAR(255) NOT NULL;");

            // Удалить столбец ContactType
            migrationBuilder.DropColumn(
                name: "ContactType",
                table: "Order");
        }
    }
}
