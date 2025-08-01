﻿// <auto-generated />
using BlazorWebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250509145850_UserTables")]
    partial class UserTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlazorWebApi.Models.CharacterRace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FactionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FactionId");

                    b.ToTable("CharacterRaces");
                });

            modelBuilder.Entity("BlazorWebApi.Models.Factions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Factions");
                });

            modelBuilder.Entity("BlazorWebApi.Models.RaceWowClass", b =>
                {
                    b.Property<int>("CharacterRaceId")
                        .HasColumnType("int");

                    b.Property<int>("WowClassId")
                        .HasColumnType("int");

                    b.HasKey("CharacterRaceId", "WowClassId");

                    b.HasIndex("WowClassId");

                    b.ToTable("RaceWowClasses");
                });

            modelBuilder.Entity("BlazorWebApi.Models.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BlazorWebApi.Models.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("WowClassId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("WowClassId");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("BlazorWebApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BlazorWebApi.Models.WowClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WowClasses");
                });

            modelBuilder.Entity("BlazorWebApi.Models.CharacterRace", b =>
                {
                    b.HasOne("BlazorWebApi.Models.Factions", "Faction")
                        .WithMany("CharacterRaces")
                        .HasForeignKey("FactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faction");
                });

            modelBuilder.Entity("BlazorWebApi.Models.RaceWowClass", b =>
                {
                    b.HasOne("BlazorWebApi.Models.CharacterRace", "CharacterRace")
                        .WithMany("RaceWowClasses")
                        .HasForeignKey("CharacterRaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorWebApi.Models.WowClass", "WowClass")
                        .WithMany("RaceWowClasses")
                        .HasForeignKey("WowClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CharacterRace");

                    b.Navigation("WowClass");
                });

            modelBuilder.Entity("BlazorWebApi.Models.Specialization", b =>
                {
                    b.HasOne("BlazorWebApi.Models.Roles", "Role")
                        .WithMany("ClassSpecializations")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorWebApi.Models.WowClass", "WowClass")
                        .WithMany("Specializations")
                        .HasForeignKey("WowClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("WowClass");
                });

            modelBuilder.Entity("BlazorWebApi.Models.CharacterRace", b =>
                {
                    b.Navigation("RaceWowClasses");
                });

            modelBuilder.Entity("BlazorWebApi.Models.Factions", b =>
                {
                    b.Navigation("CharacterRaces");
                });

            modelBuilder.Entity("BlazorWebApi.Models.Roles", b =>
                {
                    b.Navigation("ClassSpecializations");
                });

            modelBuilder.Entity("BlazorWebApi.Models.WowClass", b =>
                {
                    b.Navigation("RaceWowClasses");

                    b.Navigation("Specializations");
                });
#pragma warning restore 612, 618
        }
    }
}
