using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RVASispit.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "adresaDostave",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datumKreiranjaKorisnika",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "imePrezime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipPaketas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cenaGodisnjePretplate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    cenaMesecnePretplate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    opisPaketa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nazivPaketa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cenaRezervacije = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipPaketas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaketiKorisnikas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    godisnjaPretplata = table.Column<bool>(type: "bit", nullable: false),
                    tipPaketaID = table.Column<int>(type: "int", nullable: false),
                    korisnikID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaketiKorisnikas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaketiKorisnikas_AspNetUsers_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaketiKorisnikas_TipPaketas_tipPaketaID",
                        column: x => x.tipPaketaID,
                        principalTable: "TipPaketas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fakturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paketKorisnikaID = table.Column<int>(type: "int", nullable: false),
                    cena = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    datumIzdavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tekstFakture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    placeno = table.Column<bool>(type: "bit", nullable: false),
                    datumPlacanja = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fakturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fakturas_PaketiKorisnikas_paketKorisnikaID",
                        column: x => x.paketKorisnikaID,
                        principalTable: "PaketiKorisnikas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fakturas_paketKorisnikaID",
                table: "Fakturas",
                column: "paketKorisnikaID");

            migrationBuilder.CreateIndex(
                name: "IX_PaketiKorisnikas_korisnikID",
                table: "PaketiKorisnikas",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_PaketiKorisnikas_tipPaketaID",
                table: "PaketiKorisnikas",
                column: "tipPaketaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fakturas");

            migrationBuilder.DropTable(
                name: "PaketiKorisnikas");

            migrationBuilder.DropTable(
                name: "TipPaketas");

            migrationBuilder.DropColumn(
                name: "adresaDostave",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "datumKreiranjaKorisnika",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "imePrezime",
                table: "AspNetUsers");
        }
    }
}
