using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using StarmileFx.Api.Server.Data;

namespace StarmileFx.Api.Migrations
{
    [DbContext(typeof(BaseContext))]
    partial class BaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("StarmileFx.Models.Base.SysAuthorities", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatTime");

                    b.Property<int>("PermissionsID");

                    b.Property<bool>("State");

                    b.HasKey("ID");

                    b.ToTable("SysAuthorities");
                });

            modelBuilder.Entity("StarmileFx.Models.Base.SysEmailLogs", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<string>("Email");

                    b.Property<string>("EmailLog");

                    b.Property<bool>("State");

                    b.HasKey("ID");

                    b.ToTable("SysEmailLogs");
                });

            modelBuilder.Entity("StarmileFx.Models.Base.SysMessage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatTime");

                    b.Property<DateTime>("ReceiveDate");

                    b.Property<int>("Receiver");

                    b.Property<DateTime>("SendDate");

                    b.Property<int>("Sender");

                    b.Property<bool>("State");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("ID");

                    b.ToTable("SysLogs");
                });

            modelBuilder.Entity("StarmileFx.Models.Base.SysRoleLogs", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<string>("LoginIP");

                    b.Property<int>("RoleID");

                    b.Property<bool>("State");

                    b.HasKey("ID");

                    b.ToTable("SysRoleLogs");
                });

            modelBuilder.Entity("StarmileFx.Models.Base.SysRolePermissions", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<string>("Explain");

                    b.Property<string>("Name");

                    b.Property<int>("Permissions");

                    b.Property<bool>("State");

                    b.HasKey("ID");

                    b.ToTable("SysRolePermissions");
                });

            modelBuilder.Entity("StarmileFx.Models.Base.SysRoles", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatTime");

                    b.Property<string>("LoginName");

                    b.Property<string>("Name");

                    b.Property<int>("Permissions");

                    b.Property<string>("Pwd");

                    b.Property<bool>("State");

                    b.Property<string>("Url");

                    b.HasKey("ID");

                    b.ToTable("SysRoles");
                });
        }
    }
}
