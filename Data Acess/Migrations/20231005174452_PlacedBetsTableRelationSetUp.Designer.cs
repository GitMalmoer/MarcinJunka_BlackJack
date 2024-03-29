﻿// <auto-generated />
using System;
using Data_Acess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data_Access.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231005174452_PlacedBetsTableRelationSetUp")]
    partial class PlacedBetsTableRelationSetUp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data_Access.Entities.PlacedBet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BetAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PlacedBets");
                });

            modelBuilder.Entity("Data_Acess.Entities.GameResolution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlacedBetId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerSaldo")
                        .HasColumnType("int");

                    b.Property<string>("WinMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlacedBetId");

                    b.ToTable("GameResolutions");
                });

            modelBuilder.Entity("Data_Acess.Entities.GameResolution", b =>
                {
                    b.HasOne("Data_Access.Entities.PlacedBet", "PlacedBet")
                        .WithMany()
                        .HasForeignKey("PlacedBetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlacedBet");
                });
#pragma warning restore 612, 618
        }
    }
}
