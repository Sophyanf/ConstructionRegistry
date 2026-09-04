using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstructionRegistry.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Locality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    House = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Building = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfChenge = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KadastrIDs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KadastrNum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KadastrIDs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfObjects",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfObjects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kontragents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KontragentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KontragentShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KontragentINN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KontragentAddressID = table.Column<int>(type: "int", nullable: true),
                    NDSRate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontragents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Kontragents_Adresses_KontragentAddressID",
                        column: x => x.KontragentAddressID,
                        principalTable: "Adresses",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ContractCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractCustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractCustomerData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KontragentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractCustomers_Kontragents_KontragentID",
                        column: x => x.KontragentID,
                        principalTable: "Kontragents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponsiblPersons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonFIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonPost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonKontragentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsiblPersons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResponsiblPersons_Kontragents_PersonKontragentID",
                        column: x => x.PersonKontragentID,
                        principalTable: "Kontragents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionObjects",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjectAddressID = table.Column<int>(type: "int", nullable: true),
                    ConstructionOrganizationId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ConstructionOrganizationSubId = table.Column<int>(type: "int", nullable: true),
                    DateOfApplication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CostOfObject = table.Column<double>(type: "float", nullable: true),
                    SpendingOfObject = table.Column<double>(type: "float", nullable: true),
                    KadastrIDID = table.Column<int>(type: "int", nullable: true),
                    TypeOfObjectID = table.Column<int>(type: "int", nullable: true),
                    CustomerOrgRespPersonID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginDocuments = table.Column<int>(type: "int", nullable: false),
                    OriginDocumentsSub = table.Column<int>(type: "int", nullable: false),
                    PaymentInvoice = table.Column<int>(type: "int", nullable: true),
                    Invoice = table.Column<int>(type: "int", nullable: true),
                    ContractCustomerId = table.Column<int>(type: "int", nullable: false),
                    SubContractCustomeringCoefficients = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionObjects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionObjects_Adresses_ObjectAddressID",
                        column: x => x.ObjectAddressID,
                        principalTable: "Adresses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConstructionObjects_ContractCustomers_ContractCustomerId",
                        column: x => x.ContractCustomerId,
                        principalTable: "ContractCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructionObjects_KadastrIDs_KadastrIDID",
                        column: x => x.KadastrIDID,
                        principalTable: "KadastrIDs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConstructionObjects_Kontragents_ConstructionOrganizationId",
                        column: x => x.ConstructionOrganizationId,
                        principalTable: "Kontragents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConstructionObjects_Kontragents_ConstructionOrganizationSubId",
                        column: x => x.ConstructionOrganizationSubId,
                        principalTable: "Kontragents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConstructionObjects_Kontragents_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Kontragents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConstructionObjects_ResponsiblPersons_CustomerOrgRespPersonID",
                        column: x => x.CustomerOrgRespPersonID,
                        principalTable: "ResponsiblPersons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConstructionObjects_TypeOfObjects_TypeOfObjectID",
                        column: x => x.TypeOfObjectID,
                        principalTable: "TypeOfObjects",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionObjects_ConstructionOrganizationId",
                table: "ConstructionObjects",
                column: "ConstructionOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionObjects_ConstructionOrganizationSubId",
                table: "ConstructionObjects",
                column: "ConstructionOrganizationSubId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionObjects_ContractCustomerId",
                table: "ConstructionObjects",
                column: "ContractCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionObjects_CustomerId",
                table: "ConstructionObjects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionObjects_CustomerOrgRespPersonID",
                table: "ConstructionObjects",
                column: "CustomerOrgRespPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionObjects_KadastrIDID",
                table: "ConstructionObjects",
                column: "KadastrIDID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionObjects_ObjectAddressID",
                table: "ConstructionObjects",
                column: "ObjectAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionObjects_TypeOfObjectID",
                table: "ConstructionObjects",
                column: "TypeOfObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCustomers_KontragentID",
                table: "ContractCustomers",
                column: "KontragentID");

            migrationBuilder.CreateIndex(
                name: "IX_Kontragents_KontragentAddressID",
                table: "Kontragents",
                column: "KontragentAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsiblPersons_PersonKontragentID",
                table: "ResponsiblPersons",
                column: "PersonKontragentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructionObjects");

            migrationBuilder.DropTable(
                name: "ContractCustomers");

            migrationBuilder.DropTable(
                name: "KadastrIDs");

            migrationBuilder.DropTable(
                name: "ResponsiblPersons");

            migrationBuilder.DropTable(
                name: "TypeOfObjects");

            migrationBuilder.DropTable(
                name: "Kontragents");

            migrationBuilder.DropTable(
                name: "Adresses");
        }
    }
}
