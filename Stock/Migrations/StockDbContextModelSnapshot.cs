﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stock.Data;

#nullable disable

namespace Stock.Migrations
{
    [DbContext(typeof(StockDbContext))]
    partial class StockDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Stock.Models.Broker", b =>
                {
                    b.Property<int>("BrokerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrokerID"));

                    b.Property<string>("BrokerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrokerID");

                    b.ToTable("Brokers");
                });

            modelBuilder.Entity("Stock.Models.BrokerageAccount", b =>
                {
                    b.Property<int>("BrokerageAccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrokerageAccountID"));

                    b.Property<string>("BrokerageAccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrokerageAccountID");

                    b.ToTable("BrokerageAccounts");
                });

            modelBuilder.Entity("Stock.Models.Portfolio", b =>
                {
                    b.Property<int>("PortfolioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PortfolioID"));

                    b.Property<int>("BrokerID")
                        .HasColumnType("int");

                    b.Property<int>("BrokerageAccountID")
                        .HasColumnType("int");

                    b.Property<string>("PortfolioName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PortfolioID");

                    b.HasIndex("BrokerID");

                    b.HasIndex("BrokerageAccountID");

                    b.HasIndex("UserID");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("Stock.Models.StockActive", b =>
                {
                    b.Property<int>("StockActiveID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockActiveID"));

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("PortfolioID")
                        .HasColumnType("int");

                    b.Property<string>("SecID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StockActiveID");

                    b.HasIndex("SecID");

                    b.HasIndex("PortfolioID", "SecID")
                        .IsUnique();

                    b.ToTable("StockActives");
                });

            modelBuilder.Entity("Stock.Models.StockInformation", b =>
                {
                    b.Property<string>("SecID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ListLevel")
                        .HasColumnType("int");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("StockPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SecID");

                    b.ToTable("StockInformations");
                });

            modelBuilder.Entity("Stock.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Stock.Models.Portfolio", b =>
                {
                    b.HasOne("Stock.Models.Broker", "Broker")
                        .WithMany("Portfolios")
                        .HasForeignKey("BrokerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stock.Models.BrokerageAccount", "BrokerageAccount")
                        .WithMany("Portfolios")
                        .HasForeignKey("BrokerageAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stock.Models.User", "User")
                        .WithMany("Portfolios")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Broker");

                    b.Navigation("BrokerageAccount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Stock.Models.StockActive", b =>
                {
                    b.HasOne("Stock.Models.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stock.Models.StockInformation", "StockInformation")
                        .WithMany("StockActive")
                        .HasForeignKey("SecID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Portfolio");

                    b.Navigation("StockInformation");
                });

            modelBuilder.Entity("Stock.Models.Broker", b =>
                {
                    b.Navigation("Portfolios");
                });

            modelBuilder.Entity("Stock.Models.BrokerageAccount", b =>
                {
                    b.Navigation("Portfolios");
                });

            modelBuilder.Entity("Stock.Models.StockInformation", b =>
                {
                    b.Navigation("StockActive");
                });

            modelBuilder.Entity("Stock.Models.User", b =>
                {
                    b.Navigation("Portfolios");
                });
#pragma warning restore 612, 618
        }
    }
}
