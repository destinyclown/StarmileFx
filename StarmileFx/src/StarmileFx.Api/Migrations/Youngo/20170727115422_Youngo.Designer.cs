using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using StarmileFx.Api.Server.Data;

namespace StarmileFx.Api.Migrations.Youngo
{
    [DbContext(typeof(YoungoContext))]
    [Migration("20170727115422_Youngo")]
    partial class Youngo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("StarmileFx.Models.Wap.OrderParent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Area");

                    b.Property<string>("City");

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("CustomerID");

                    b.Property<string>("CustomerRemarks");

                    b.Property<DateTime?>("DeliveryTime");

                    b.Property<float>("ExpressPrice");

                    b.Property<DateTime?>("FinishTime");

                    b.Property<int>("Number");

                    b.Property<string>("OrderID");

                    b.Property<int>("OrderState");

                    b.Property<float>("PackPrice");

                    b.Property<DateTime?>("PayTime");

                    b.Property<int>("PaymentType");

                    b.Property<string>("Phone");

                    b.Property<string>("ProductID");

                    b.Property<string>("Province");

                    b.Property<string>("ReceiveName");

                    b.Property<float>("TotalPrice");

                    b.Property<string>("TraceID");

                    b.HasKey("ID");

                    b.ToTable("OrderParent");
                });

            modelBuilder.Entity("StarmileFx.Models.Wap.ProductComment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreatTime");

                    b.Property<string>("OrderID");

                    b.Property<string>("ProductID");

                    b.Property<int?>("Reply");

                    b.Property<int>("UserName");

                    b.HasKey("ID");

                    b.ToTable("ProductComment");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("CustomerType");

                    b.Property<int>("Integral");

                    b.Property<bool>("State");

                    b.Property<string>("UserName");

                    b.Property<string>("WeCharKey");

                    b.HasKey("ID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.CustomerComment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("CustomerID");

                    b.Property<string>("OrderID");

                    b.Property<string>("ProductID");

                    b.Property<int?>("Reply");

                    b.HasKey("ID");

                    b.ToTable("CustomerComment");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.CustomerSign", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("CustomerID");

                    b.Property<int>("Integral");

                    b.Property<string>("Mode");

                    b.HasKey("ID");

                    b.ToTable("CustomerSign");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.DeliveryAddress", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Area");

                    b.Property<string>("City");

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("CustomerID");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Phone");

                    b.Property<string>("Province");

                    b.Property<string>("ReceiveName");

                    b.Property<string>("Zip");

                    b.HasKey("ID");

                    b.ToTable("DeliveryAddress");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.Express", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<string>("Explain");

                    b.Property<string>("ExpressCode");

                    b.Property<string>("ExpressName");

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsStop");

                    b.HasKey("ID");

                    b.ToTable("Express");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.Feedback", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatTime");

                    b.Property<string>("Email");

                    b.Property<string>("Phone");

                    b.HasKey("ID");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.OffLineOrderDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("Number");

                    b.Property<string>("OrderID");

                    b.Property<string>("ProductID");

                    b.HasKey("ID");

                    b.ToTable("OffLineOrderDetail");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.OffLineOrderParent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("CollectPrice");

                    b.Property<DateTime>("CreatTime");

                    b.Property<float>("DiscountPrice");

                    b.Property<string>("OrderID");

                    b.Property<int>("OrderState");

                    b.Property<DateTime>("PayTime");

                    b.Property<int>("PaymentType");

                    b.Property<float>("TotalPrice");

                    b.HasKey("ID");

                    b.ToTable("OffLineOrderParent");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.OnLineOrderDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("Number");

                    b.Property<string>("OrderID");

                    b.Property<string>("ProductID");

                    b.HasKey("ID");

                    b.ToTable("OnLineOrderDetail");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.OnLineOrderParent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BatchProcessingID");

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("CustomerID");

                    b.Property<string>("CustomerRemarks");

                    b.Property<int>("DeliveryAddressID");

                    b.Property<DateTime?>("DeliveryTime");

                    b.Property<string>("DeliveryUser");

                    b.Property<string>("ExpressCode");

                    b.Property<float>("ExpressPrice");

                    b.Property<DateTime?>("FinishTime");

                    b.Property<bool>("IsDelay");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDelivery");

                    b.Property<int>("IsFragile");

                    b.Property<bool>("IsRemoteArea");

                    b.Property<string>("OrderID");

                    b.Property<int>("OrderState");

                    b.Property<int>("OrderType");

                    b.Property<float>("PackPrice");

                    b.Property<DateTime?>("PayTime");

                    b.Property<int>("PaymentType");

                    b.Property<float>("TotalPrice");

                    b.Property<string>("TraceID");

                    b.Property<float>("Weight");

                    b.HasKey("ID");

                    b.ToTable("OnLineOrderParent");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.OrderEstablish", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("OriginalOrderID");

                    b.HasKey("ID");

                    b.ToTable("OrderEstablish");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand");

                    b.Property<string>("BrandIntroduce");

                    b.Property<string>("CnName");

                    b.Property<float>("CostPrice");

                    b.Property<DateTime>("CreatTime");

                    b.Property<string>("EnName");

                    b.Property<string>("ExpressCode");

                    b.Property<string>("Introduce");

                    b.Property<bool>("IsClearStock");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsOutOfStock");

                    b.Property<bool>("IsTop");

                    b.Property<string>("Label");

                    b.Property<DateTime?>("OnlineTime");

                    b.Property<string>("ProductID");

                    b.Property<float>("PurchasePrice");

                    b.Property<string>("Remarks");

                    b.Property<int>("SalesVolume");

                    b.Property<bool>("State");

                    b.Property<int>("Stock");

                    b.Property<int>("Type");

                    b.Property<float>("Weight");

                    b.HasKey("ID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.ProductType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatTime");

                    b.Property<string>("Introduce");

                    b.Property<bool>("State");

                    b.Property<string>("TypeName");

                    b.HasKey("ID");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.Resources", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreatTime");

                    b.Property<int?>("ProductCommentID");

                    b.Property<string>("ProductID");

                    b.Property<string>("ResourcesCode");

                    b.Property<int>("Sort");

                    b.Property<int>("Type");

                    b.HasKey("ID");

                    b.HasIndex("ProductCommentID");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.ServiceRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("CustomerID");

                    b.Property<bool>("IsHandle");

                    b.Property<string>("OrderID");

                    b.Property<string>("Remarks");

                    b.Property<int>("Type");

                    b.HasKey("ID");

                    b.ToTable("ServiceRecord");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.SKUEstablish", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("OriginalSKU");

                    b.HasKey("ID");

                    b.ToTable("SKUEstablish");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.SreachHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<int?>("CustomerID");

                    b.Property<string>("Keyword");

                    b.HasKey("ID");

                    b.ToTable("SreachHistory");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.TransactionRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<string>("OrderID");

                    b.Property<float>("TotalPrice");

                    b.Property<string>("TransactionID");

                    b.Property<int>("Type");

                    b.HasKey("ID");

                    b.ToTable("TransactionRecord");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.ViewHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<int?>("CustomerID");

                    b.Property<string>("ProductID");

                    b.HasKey("ID");

                    b.ToTable("ViewHistory");
                });

            modelBuilder.Entity("StarmileFx.Models.Youngo.Resources", b =>
                {
                    b.HasOne("StarmileFx.Models.Wap.ProductComment")
                        .WithMany("ResourcesList")
                        .HasForeignKey("ProductCommentID");
                });
        }
    }
}
