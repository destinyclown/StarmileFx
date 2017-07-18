using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarmileFx.Api.Migrations.Youngo
{
    public partial class Youngo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderParent",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Address = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    CustomerRemarks = table.Column<string>(nullable: true),
                    DeliveryTime = table.Column<DateTime>(nullable: true),
                    ExpressPrice = table.Column<float>(nullable: false),
                    FinishTime = table.Column<DateTime>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    OrderID = table.Column<string>(nullable: true),
                    OrderState = table.Column<int>(nullable: false),
                    PackPrice = table.Column<float>(nullable: false),
                    PayTime = table.Column<DateTime>(nullable: true),
                    PaymentType = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    ProductID = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    ReceiveName = table.Column<string>(nullable: true),
                    TotalPrice = table.Column<float>(nullable: false),
                    TraceID = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderParent", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    CustomerType = table.Column<int>(nullable: false),
                    Integral = table.Column<int>(nullable: false),
                    State = table.Column<bool>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    WeCharKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerComment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Comment = table.Column<string>(nullable: true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    OrderID = table.Column<string>(nullable: true),
                    ProductID = table.Column<string>(nullable: true),
                    Reply = table.Column<int>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerComment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSign",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    Integral = table.Column<int>(nullable: false),
                    Mode = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSign", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAddress",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Address = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    ReceiveName = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddress", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Content = table.Column<string>(nullable: true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OffLineOrderDetail",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    OrderID = table.Column<string>(nullable: true),
                    ProductID = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffLineOrderDetail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OffLineOrderParent",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CollectPrice = table.Column<float>(nullable: false),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    DiscountPrice = table.Column<float>(nullable: false),
                    OrderID = table.Column<string>(nullable: true),
                    OrderState = table.Column<int>(nullable: false),
                    PayTime = table.Column<DateTime>(nullable: false),
                    PaymentType = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<float>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffLineOrderParent", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OnLineOrderDetail",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    OrderID = table.Column<string>(nullable: true),
                    ProductID = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnLineOrderDetail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OnLineOrderParent",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BatchProcessingID = table.Column<string>(nullable: true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    CustomerRemarks = table.Column<string>(nullable: true),
                    DeliveryAddressID = table.Column<int>(nullable: false),
                    DeliveryTime = table.Column<DateTime>(nullable: true),
                    DeliveryUser = table.Column<string>(nullable: true),
                    ExpressPrice = table.Column<float>(nullable: false),
                    FinishTime = table.Column<DateTime>(nullable: true),
                    IsDelay = table.Column<bool>(nullable: false),
                    IsDelet = table.Column<bool>(nullable: false),
                    IsDelivery = table.Column<bool>(nullable: false),
                    IsFragile = table.Column<int>(nullable: false),
                    IsRemoteArea = table.Column<bool>(nullable: false),
                    OrderID = table.Column<string>(nullable: true),
                    OrderState = table.Column<int>(nullable: false),
                    OrderType = table.Column<int>(nullable: false),
                    PackPrice = table.Column<float>(nullable: false),
                    PayTime = table.Column<DateTime>(nullable: true),
                    PaymentType = table.Column<int>(nullable: false),
                    PostID = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<float>(nullable: false),
                    TraceID = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnLineOrderParent", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderEstablish",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    OriginalOrderID = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEstablish", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    IsStop = table.Column<bool>(nullable: false),
                    PostCode = table.Column<string>(nullable: true),
                    PostName = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CnName = table.Column<string>(nullable: true),
                    CostPrice = table.Column<float>(nullable: false),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    EnName = table.Column<string>(nullable: true),
                    Introduce = table.Column<string>(nullable: true),
                    IsClearStock = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsOutOfStock = table.Column<bool>(nullable: false),
                    IsTop = table.Column<bool>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    OnlineTime = table.Column<DateTime>(nullable: true),
                    ProductID = table.Column<string>(nullable: true),
                    PurchasePrice = table.Column<float>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    State = table.Column<bool>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Code = table.Column<string>(nullable: true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    Introduce = table.Column<string>(nullable: true),
                    State = table.Column<bool>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SKUEstablish",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    OriginalSKU = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUEstablish", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SreachHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<int>(nullable: true),
                    Keyword = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SreachHistory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ViewHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<int>(nullable: true),
                    ProductID = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewHistory", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderParent");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "CustomerComment");

            migrationBuilder.DropTable(
                name: "CustomerSign");

            migrationBuilder.DropTable(
                name: "DeliveryAddress");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "OffLineOrderDetail");

            migrationBuilder.DropTable(
                name: "OffLineOrderParent");

            migrationBuilder.DropTable(
                name: "OnLineOrderDetail");

            migrationBuilder.DropTable(
                name: "OnLineOrderParent");

            migrationBuilder.DropTable(
                name: "OrderEstablish");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "SKUEstablish");

            migrationBuilder.DropTable(
                name: "SreachHistory");

            migrationBuilder.DropTable(
                name: "ViewHistory");
        }
    }
}
