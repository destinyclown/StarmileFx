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
                    TraceID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderParent", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductComment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Comment = table.Column<string>(nullable: true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    OrderID = table.Column<string>(nullable: true),
                    ProductID = table.Column<string>(nullable: true),
                    Reply = table.Column<int>(nullable: true),
                    UserName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComment", x => x.ID);
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
                    Reply = table.Column<int>(nullable: true)
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
                    Mode = table.Column<string>(nullable: true)
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
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddress", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Express",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    Explain = table.Column<string>(nullable: true),
                    ExpressCode = table.Column<string>(nullable: true),
                    ExpressName = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsStop = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Express", x => x.ID);
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
                    Phone = table.Column<string>(nullable: true)
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
                    ProductID = table.Column<string>(nullable: true)
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
                    TotalPrice = table.Column<float>(nullable: false)
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
                    ProductID = table.Column<string>(nullable: true)
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
                    ExpressCode = table.Column<string>(nullable: true),
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
                    TotalPrice = table.Column<float>(nullable: false),
                    TraceID = table.Column<string>(nullable: true),
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
                    OriginalOrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEstablish", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Brand = table.Column<string>(nullable: true),
                    BrandIntroduce = table.Column<string>(nullable: true),
                    CnName = table.Column<string>(nullable: true),
                    CostPrice = table.Column<float>(nullable: false),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    EnName = table.Column<string>(nullable: true),
                    ExpressCode = table.Column<string>(nullable: true),
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
                    SalesVolume = table.Column<int>(nullable: false),
                    State = table.Column<bool>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
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
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRecord",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Content = table.Column<string>(nullable: true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    IsHandle = table.Column<bool>(nullable: false),
                    OrderID = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRecord", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SKUEstablish",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    OriginalSKU = table.Column<int>(nullable: false)
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
                    Keyword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SreachHistory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TransactionRecord",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    OrderID = table.Column<string>(nullable: true),
                    TotalPrice = table.Column<float>(nullable: false),
                    TransactionID = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRecord", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ViewHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<int>(nullable: true),
                    ProductID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewHistory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Address = table.Column<string>(nullable: true),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    ProductCommentID = table.Column<int>(nullable: true),
                    ProductID = table.Column<string>(nullable: true),
                    ResourcesCode = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Resources_ProductComment_ProductCommentID",
                        column: x => x.ProductCommentID,
                        principalTable: "ProductComment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ProductCommentID",
                table: "Resources",
                column: "ProductCommentID");
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
                name: "Express");

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
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "ServiceRecord");

            migrationBuilder.DropTable(
                name: "SKUEstablish");

            migrationBuilder.DropTable(
                name: "SreachHistory");

            migrationBuilder.DropTable(
                name: "TransactionRecord");

            migrationBuilder.DropTable(
                name: "ViewHistory");

            migrationBuilder.DropTable(
                name: "ProductComment");
        }
    }
}
