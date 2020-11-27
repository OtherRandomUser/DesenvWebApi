﻿// <auto-generated />
using System;
using DesenvWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesenvWebApi.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201127014705_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DesenvWebApi.Domain.Curriculum", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Curriculums");
                });

            modelBuilder.Entity("DesenvWebApi.Domain.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("DesenvWebApi.Domain.SubjectCurriculum", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CurriculumId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CurriculumId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SubjectCurriculums");
                });

            modelBuilder.Entity("DesenvWebApi.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("CurriculumId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CurriculumId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DesenvWebApi.Domain.SubjectCurriculum", b =>
                {
                    b.HasOne("DesenvWebApi.Domain.Curriculum", "Curriculum")
                        .WithMany("Subjects")
                        .HasForeignKey("CurriculumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DesenvWebApi.Domain.Subject", "Subject")
                        .WithMany("Curriculums")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DesenvWebApi.Domain.User", b =>
                {
                    b.HasOne("DesenvWebApi.Domain.Curriculum", "Curriculum")
                        .WithMany()
                        .HasForeignKey("CurriculumId");
                });
#pragma warning restore 612, 618
        }
    }
}
