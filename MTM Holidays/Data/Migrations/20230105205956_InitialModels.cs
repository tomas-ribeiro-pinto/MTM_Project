using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTM_Holidays.Data.Migrations
{
    public partial class InitialModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Street = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Town = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    PostCode = table.Column<string>(type: "TEXT", nullable: false),
                    County = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Country = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Region = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CardPayment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardNumber = table.Column<int>(type: "INTEGER", maxLength: 10, nullable: false),
                    SecurityCode = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPayment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<int>(type: "INTEGER", maxLength: 11, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AddressID = table.Column<int>(type: "INTEGER", nullable: false),
                    CardPaymentID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customer_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customer_CardPayment_CardPaymentID",
                        column: x => x.CardPaymentID,
                        principalTable: "CardPayment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Region = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    AccommodationType = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    OriginAddressID = table.Column<int>(type: "INTEGER", nullable: false),
                    DestinationAddressID = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Holidays_Address_DestinationAddressID",
                        column: x => x.DestinationAddressID,
                        principalTable: "Address",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Holidays_Address_OriginAddressID",
                        column: x => x.OriginAddressID,
                        principalTable: "Address",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Holidays_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Order_Holidays",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Discount = table.Column<double>(type: "REAL", nullable: false),
                    IsPaid = table.Column<bool>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HolidayID = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderID = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscountCodeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Holidays", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Order_Holidays_DiscountCodes_DiscountCodeID",
                        column: x => x.DiscountCodeID,
                        principalTable: "DiscountCodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Holidays_Holidays_HolidayID",
                        column: x => x.HolidayID,
                        principalTable: "Holidays",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Holidays_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    PictureID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    URL = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    HolidayID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.PictureID);
                    table.ForeignKey(
                        name: "FK_Pictures_Holidays_HolidayID",
                        column: x => x.HolidayID,
                        principalTable: "Holidays",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AddressID",
                table: "Customer",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CardPaymentID",
                table: "Customer",
                column: "CardPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_DestinationAddressID",
                table: "Holidays",
                column: "DestinationAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_OrderID",
                table: "Holidays",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_OriginAddressID",
                table: "Holidays",
                column: "OriginAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Holidays_DiscountCodeID",
                table: "Order_Holidays",
                column: "DiscountCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Holidays_HolidayID",
                table: "Order_Holidays",
                column: "HolidayID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Holidays_OrderID",
                table: "Order_Holidays",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_HolidayID",
                table: "Pictures",
                column: "HolidayID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_Holidays");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CardPayment");
        }
    }
}
