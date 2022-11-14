﻿// <auto-generated />
using System;
using User.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Identity.Infrastructure.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20221019193428_CitizenshipUpdate")]
    partial class CitizenshipUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Identity.Domain.Entities.UserDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AvatarLink")
                        .HasColumnType("VARCHAR(60)");

                    b.Property<string>("Citizenship")
                        .HasColumnType("VARCHAR(60)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("InstagramLink")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<DateTime>("LastModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(68)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("VARCHAR(13)");

                    b.Property<DateTime>("RegistrationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
