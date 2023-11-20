﻿// <auto-generated />
using System;
using GymWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GymWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231120185649_nuevamigracion")]
    partial class nuevamigracion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GymWeb.Models.Alumno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClaseRefId")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dni")
                        .HasColumnType("int");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClaseRefId");

                    b.ToTable("Alumno");
                });

            modelBuilder.Entity("GymWeb.Models.Calendario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClaseRefId")
                        .HasColumnType("int");

                    b.Property<string>("Dia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("HoraFin")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("ClaseRefId");

                    b.ToTable("Calendario");
                });

            modelBuilder.Entity("GymWeb.Models.Clase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Dificultad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instructor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clase");
                });

            modelBuilder.Entity("GymWeb.Models.CondicionPago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("CondicionPago");
                });

            modelBuilder.Entity("GymWeb.Models.PrecioMes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClaseRefId")
                        .HasColumnType("int");

                    b.Property<int>("Cuota")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClaseRefId");

                    b.ToTable("PrecioMes");
                });

            modelBuilder.Entity("GymWeb.Models.Alumno", b =>
                {
                    b.HasOne("GymWeb.Models.Clase", "Clases")
                        .WithMany()
                        .HasForeignKey("ClaseRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clases");
                });

            modelBuilder.Entity("GymWeb.Models.Calendario", b =>
                {
                    b.HasOne("GymWeb.Models.Clase", "Clases")
                        .WithMany()
                        .HasForeignKey("ClaseRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clases");
                });

            modelBuilder.Entity("GymWeb.Models.PrecioMes", b =>
                {
                    b.HasOne("GymWeb.Models.Clase", "Clases")
                        .WithMany()
                        .HasForeignKey("ClaseRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clases");
                });
#pragma warning restore 612, 618
        }
    }
}
