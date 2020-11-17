﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyEuroleagueMVCAspNetCore.Models;

namespace MyEuroleagueMVCAspNetCore.Migrations
{
    [DbContext(typeof(Euroleague2020_21ASPDBContext))]
    [Migration("20201106082008_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyEuroleagueMVCAspNetCore.Models.Matches", b =>
                {
                    b.Property<int>("MatchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AwayPointsScored")
                        .HasColumnType("int");

                    b.Property<int?>("EndOfFourthPeriodPoints")
                        .HasColumnType("int");

                    b.Property<int>("HomePointsScored")
                        .HasColumnType("int");

                    b.Property<int>("RoundNo")
                        .HasColumnType("int");

                    b.Property<string>("away_Team")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("hadExtraTime")
                        .HasColumnType("bit");

                    b.Property<string>("Home_Team")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("MatchID");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("MyEuroleagueMVCAspNetCore.Models.Teams", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Coach")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("TeamID");

                    b.ToTable("Team");
                });
#pragma warning restore 612, 618
        }
    }
}