using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShopSystem.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    İsim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Posta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profesyoneller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    İsim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Posta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesyoneller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sure = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servisler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfesyonelServisler",
                columns: table => new
                {
                    ProfesyonelId = table.Column<int>(type: "int", nullable: false),
                    ServisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesyonelServisler", x => new { x.ProfesyonelId, x.ServisId });
                    table.ForeignKey(
                        name: "FK_ProfesyonelServisler_Profesyoneller_ProfesyonelId",
                        column: x => x.ProfesyonelId,
                        principalTable: "Profesyoneller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesyonelServisler_Servisler_ServisId",
                        column: x => x.ServisId,
                        principalTable: "Servisler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervasyonlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfesyonelId = table.Column<int>(type: "int", nullable: false),
                    ServisId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RezervasyonDurumu = table.Column<bool>(type: "bit", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TarihGuncelleme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Yenilik = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervasyonlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervasyonlar_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervasyonlar_Profesyoneller_ProfesyonelId",
                        column: x => x.ProfesyonelId,
                        principalTable: "Profesyoneller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervasyonlar_Servisler_ServisId",
                        column: x => x.ServisId,
                        principalTable: "Servisler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfesyonelServisler_ServisId",
                table: "ProfesyonelServisler",
                column: "ServisId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyonlar_CustomerId",
                table: "Rezervasyonlar",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyonlar_ProfesyonelId",
                table: "Rezervasyonlar",
                column: "ProfesyonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyonlar_ServisId",
                table: "Rezervasyonlar",
                column: "ServisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfesyonelServisler");

            migrationBuilder.DropTable(
                name: "Rezervasyonlar");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Profesyoneller");

            migrationBuilder.DropTable(
                name: "Servisler");
        }
    }
}
