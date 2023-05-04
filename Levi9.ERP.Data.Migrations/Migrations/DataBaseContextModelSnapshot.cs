﻿// <auto-generated />
using System;
using Levi9.ERP.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Levi9.ERP.Data.Migrations.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Levi9.ERP.Domain.Model.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<Guid>("GlobalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PriceListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PriceListId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("GlobalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.Price", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PriceListId")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<Guid>("GlobalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<float>("PriceValue")
                        .HasColumnType("real");

                    b.HasKey("ProductId", "PriceListId");

                    b.HasIndex("PriceListId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.PriceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("GlobalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PriceLists");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("int");

                    b.Property<Guid>("GlobalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.ProductDocument", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<float>("PriceValue")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "DocumentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("ProductDocuments");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.Client", b =>
                {
                    b.HasOne("Levi9.ERP.Domain.Model.PriceList", "PriceList")
                        .WithMany()
                        .HasForeignKey("PriceListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriceList");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.Document", b =>
                {
                    b.HasOne("Levi9.ERP.Domain.Model.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.Price", b =>
                {
                    b.HasOne("Levi9.ERP.Domain.Model.PriceList", "PriceList")
                        .WithMany("Prices")
                        .HasForeignKey("PriceListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Levi9.ERP.Domain.Model.Product", "Product")
                        .WithMany("Prices")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriceList");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.ProductDocument", b =>
                {
                    b.HasOne("Levi9.ERP.Domain.Model.Document", "Document")
                        .WithMany("ProductDocuments")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Levi9.ERP.Domain.Model.Product", "Product")
                        .WithMany("ProductDocuments")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.Document", b =>
                {
                    b.Navigation("ProductDocuments");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.PriceList", b =>
                {
                    b.Navigation("Prices");
                });

            modelBuilder.Entity("Levi9.ERP.Domain.Model.Product", b =>
                {
                    b.Navigation("Prices");

                    b.Navigation("ProductDocuments");
                });
#pragma warning restore 612, 618
        }
    }
}
