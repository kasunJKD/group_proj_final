using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class testingwithaddedtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Customizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomizationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Customizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customizations_Customizations_CustomizationsId",
                        column: x => x.CustomizationsId,
                        principalTable: "Customizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingOrderId = table.Column<int>(type: "int", nullable: false),
                    Estimated_DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableCount = table.Column<int>(type: "int", nullable: false),
                    Price_Per_Unit = table.Column<double>(type: "float", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaneModelsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaneModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuselageInventoryId = table.Column<int>(type: "int", nullable: false),
                    WingsInventoryId = table.Column<int>(type: "int", nullable: false),
                    Wing_Count = table.Column<int>(type: "int", nullable: false),
                    EngineInventoryId = table.Column<int>(type: "int", nullable: false),
                    Engine_Count = table.Column<int>(type: "int", nullable: false),
                    Max_Range = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    BasePrice = table.Column<double>(type: "float", nullable: false),
                    Image_url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaneModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaneModels_Inventory_EngineInventoryId",
                        column: x => x.EngineInventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PlaneModels_Inventory_FuselageInventoryId",
                        column: x => x.FuselageInventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PlaneModels_Inventory_WingsInventoryId",
                        column: x => x.WingsInventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderedModelId = table.Column<int>(type: "int", nullable: false),
                    CustomOrderId = table.Column<int>(type: "int", nullable: false),
                    Customization_Price = table.Column<double>(type: "float", nullable: false),
                    Total_Price = table.Column<double>(type: "float", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Order_Customizations_CustomOrderId",
                        column: x => x.CustomOrderId,
                        principalTable: "Order_Customizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_PlaneModels_OrderedModelId",
                        column: x => x.OrderedModelId,
                        principalTable: "PlaneModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manufacture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DeliveryDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manufacture_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ShippingOrderId",
                table: "Shipping",
                column: "ShippingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_PlaneModelsId",
                table: "Inventory",
                column: "PlaneModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacture_OrderId",
                table: "Manufacture",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomOrderId",
                table: "Order",
                column: "CustomOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderedModelId",
                table: "Order",
                column: "OrderedModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Customizations_CustomizationsId",
                table: "Order_Customizations",
                column: "CustomizationsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaneModels_EngineInventoryId",
                table: "PlaneModels",
                column: "EngineInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaneModels_FuselageInventoryId",
                table: "PlaneModels",
                column: "FuselageInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaneModels_WingsInventoryId",
                table: "PlaneModels",
                column: "WingsInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Order_ShippingOrderId",
                table: "Shipping",
                column: "ShippingOrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_PlaneModels_PlaneModelsId",
                table: "Inventory",
                column: "PlaneModelsId",
                principalTable: "PlaneModels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_PlaneModels_PlaneModelsId",
                table: "Inventory");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "Manufacture");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Order_Customizations");

            migrationBuilder.DropTable(
                name: "Customizations");

            migrationBuilder.DropTable(
                name: "PlaneModels");

            migrationBuilder.DropTable(
                name: "Inventory");
        }
    }
}
