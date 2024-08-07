﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ordering.Infrastructure.Persistence;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    [DbContext(typeof(OrderingDbContext))]
    [Migration("20240721213046_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ordering.Core.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address_line1");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address_line2");

                    b.Property<string>("CVV")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("cvv");

                    b.Property<string>("CardName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("card_name");

                    b.Property<string>("CardNumber")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("card_number");

                    b.Property<string>("CardType")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("card_type");

                    b.Property<string>("City")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("country");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email_address");

                    b.Property<string>("Expiration")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("expiration");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("first_name");

                    b.Property<bool?>("IsCanceled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_canceled");

                    b.Property<bool?>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<bool?>("IsPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_paid");

                    b.Property<bool?>("IsShipped")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_shipped");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_modified_by");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_modified_date");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("last_name");

                    b.Property<int?>("PaymentMethod")
                        .HasColumnType("int")
                        .HasColumnName("payment_method");

                    b.Property<string>("PaymentProviderUsed")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("payment_provider_used");

                    b.Property<string>("PaymentReference")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("payment_reference");

                    b.Property<string>("ShippingDetails")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("shipping_details");

                    b.Property<string>("State")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("state");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18,4)")
                        .HasColumnName("total_pricae");

                    b.Property<string>("UserName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("user_name");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("zip_code");

                    b.HasKey("Id")
                        .HasName("PK_order");

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("Ordering.Core.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("oderdetail_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)")
                        .HasColumnName("price");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("product_id");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("product_name");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("PK_orderdetail");

                    b.HasIndex("OrderId");

                    b.ToTable("orderdetail", (string)null);
                });

            modelBuilder.Entity("Ordering.Core.Entities.OrderDetail", b =>
                {
                    b.HasOne("Ordering.Core.Entities.Order", null)
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ordering.Core.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
