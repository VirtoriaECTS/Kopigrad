using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kopigrad.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ContactType",
                columns: table => new
                {
                    ContactTypeId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContactTypeName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ContactTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "material",
                columns: table => new
                {
                    IdMaterial = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nameMaterial = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Count = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdMaterial);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "service",
                columns: table => new
                {
                    idService = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nameService = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConditionText = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Time = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<byte[]>(type: "longblob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idService);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    idStatus = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameStatus = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idStatus);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "storypdf",
                columns: table => new
                {
                    idStory = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nameFile = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AllPrice = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idStory);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nameUser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hashPassworld = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Salt = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idUser);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "miniservice",
                columns: table => new
                {
                    idMiniService = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idService = table.Column<int>(type: "int(11)", nullable: false),
                    nameMiniServise = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopName = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BottomName = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idMiniService);
                    table.ForeignKey(
                        name: "miniservice_ibfk_1",
                        column: x => x.idService,
                        principalTable: "service",
                        principalColumn: "idService");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "pagepdf",
                columns: table => new
                {
                    idPage = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idStory = table.Column<int>(type: "int(11)", nullable: false),
                    Size = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idPage);
                    table.ForeignKey(
                        name: "pagepdf_ibfk_1",
                        column: x => x.idStory,
                        principalTable: "storypdf",
                        principalColumn: "idStory");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "columnnames",
                columns: table => new
                {
                    idColumnNames = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idMiniService = table.Column<int>(type: "int(11)", nullable: false),
                    nameColumn = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idColumnNames);
                    table.ForeignKey(
                        name: "columnnames_ibfk_1",
                        column: x => x.idMiniService,
                        principalTable: "miniservice",
                        principalColumn: "idMiniService");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "tableminiservice",
                columns: table => new
                {
                    idTableMiniService = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idMiniService = table.Column<int>(type: "int(11)", nullable: false),
                    idMaterial = table.Column<int>(type: "int(100)", nullable: false),
                    idColumnName = table.Column<int>(type: "int(11)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idTableMiniService);
                    table.ForeignKey(
                        name: "tableminiservice_ibfk_1",
                        column: x => x.idMiniService,
                        principalTable: "miniservice",
                        principalColumn: "idMiniService");
                    table.ForeignKey(
                        name: "tableminiservice_ibfk_2",
                        column: x => x.idMaterial,
                        principalTable: "material",
                        principalColumn: "IdMaterial");
                    table.ForeignKey(
                        name: "tableminiservice_ibfk_3",
                        column: x => x.idColumnName,
                        principalTable: "columnnames",
                        principalColumn: "idColumnNames");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    IdOrder = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Contact = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameUser = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idStatus = table.Column<int>(type: "int(11)", nullable: false),
                    DataOrder = table.Column<DateTime>(type: "datetime", nullable: false),
                    ContactTypeId = table.Column<int>(type: "int(11)", nullable: false),
                    idTableMiniService = table.Column<int>(type: "int(11)", nullable: false),
                    Count = table.Column<int>(type: "int(11)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdOrder);
                    table.ForeignKey(
                        name: "fk_contacttype",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactType",
                        principalColumn: "ContactTypeId");
                    table.ForeignKey(
                        name: "fk_order_tableminiservice",
                        column: x => x.idTableMiniService,
                        principalTable: "tableminiservice",
                        principalColumn: "idTableMiniService");
                    table.ForeignKey(
                        name: "order_ibfk_1",
                        column: x => x.idStatus,
                        principalTable: "status",
                        principalColumn: "idStatus");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "orderitems",
                columns: table => new
                {
                    idOrderItems = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdOrder = table.Column<int>(type: "int(11)", nullable: false),
                    FilePath = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idOrderItems);
                    table.ForeignKey(
                        name: "fk_orderitems_order",
                        column: x => x.IdOrder,
                        principalTable: "order",
                        principalColumn: "IdOrder",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "columnnames_ibfk_1",
                table: "columnnames",
                column: "idMiniService");

            migrationBuilder.CreateIndex(
                name: "idService",
                table: "miniservice",
                column: "idService");

            migrationBuilder.CreateIndex(
                name: "fk_contacttype",
                table: "order",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "idStatus",
                table: "order",
                column: "idStatus");

            migrationBuilder.CreateIndex(
                name: "idx_idTableMiniService",
                table: "order",
                column: "idTableMiniService");

            migrationBuilder.CreateIndex(
                name: "IdRequst",
                table: "orderitems",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "idStory",
                table: "pagepdf",
                column: "idStory");

            migrationBuilder.CreateIndex(
                name: "idColumnName",
                table: "tableminiservice",
                column: "idColumnName");

            migrationBuilder.CreateIndex(
                name: "idMaterial",
                table: "tableminiservice",
                column: "idMaterial");

            migrationBuilder.CreateIndex(
                name: "idMiniService",
                table: "tableminiservice",
                column: "idMiniService");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderitems");

            migrationBuilder.DropTable(
                name: "pagepdf");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "storypdf");

            migrationBuilder.DropTable(
                name: "ContactType");

            migrationBuilder.DropTable(
                name: "tableminiservice");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "material");

            migrationBuilder.DropTable(
                name: "columnnames");

            migrationBuilder.DropTable(
                name: "miniservice");

            migrationBuilder.DropTable(
                name: "service");
        }
    }
}
