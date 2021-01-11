﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ehpad.ORM;

namespace ehpad.ORM.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ehpad.ORM.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("ehpad.ORM.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("ehpad.ORM.Injection", b =>
                {
                    b.Property<int>("PeopleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VaccineId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ReminderDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("VaccineDate")
                        .HasColumnType("TEXT");

                    b.HasKey("PeopleId", "VaccineId");

                    b.HasIndex("VaccineId");

                    b.ToTable("Injections");
                });

            modelBuilder.Entity("ehpad.ORM.People", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Birth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Peoples");
                });

            modelBuilder.Entity("ehpad.ORM.Vaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BrandId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DrugId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Lot")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("DrugId");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("ehpad.ORM.Injection", b =>
                {
                    b.HasOne("ehpad.ORM.People", "People")
                        .WithMany("Injections")
                        .HasForeignKey("PeopleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ehpad.ORM.Vaccine", "Vaccine")
                        .WithMany("Injections")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("People");

                    b.Navigation("Vaccine");
                });

            modelBuilder.Entity("ehpad.ORM.Vaccine", b =>
                {
                    b.HasOne("ehpad.ORM.Brand", "Brand")
                        .WithMany("Vaccinnes")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ehpad.ORM.Drug", "Drug")
                        .WithMany("Vaccinnes")
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Drug");
                });

            modelBuilder.Entity("ehpad.ORM.Brand", b =>
                {
                    b.Navigation("Vaccinnes");
                });

            modelBuilder.Entity("ehpad.ORM.Drug", b =>
                {
                    b.Navigation("Vaccinnes");
                });

            modelBuilder.Entity("ehpad.ORM.People", b =>
                {
                    b.Navigation("Injections");
                });

            modelBuilder.Entity("ehpad.ORM.Vaccine", b =>
                {
                    b.Navigation("Injections");
                });
#pragma warning restore 612, 618
        }
    }
}
