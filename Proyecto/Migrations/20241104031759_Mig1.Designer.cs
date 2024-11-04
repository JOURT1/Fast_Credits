﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proyecto.Data;

#nullable disable

namespace Proyecto.Migrations
{
    [DbContext(typeof(ProyectoContext))]
    [Migration("20241104031759_Mig1")]
    partial class Mig1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Proyecto.Models.Civil", b =>
                {
                    b.Property<int>("IdCivil")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCivil"));

                    b.Property<bool>("Casado")
                        .HasColumnType("bit");

                    b.Property<int?>("Hijos")
                        .HasColumnType("int");

                    b.Property<int>("IdPersona")
                        .HasColumnType("int");

                    b.HasKey("IdCivil");

                    b.HasIndex("IdPersona");

                    b.ToTable("Civil");
                });

            modelBuilder.Entity("Proyecto.Models.Legal", b =>
                {
                    b.Property<int>("IdLegal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLegal"));

                    b.Property<bool>("Antecedentes_Penales")
                        .HasColumnType("bit");

                    b.Property<bool>("Denuncias")
                        .HasColumnType("bit");

                    b.Property<bool>("Fraudes")
                        .HasColumnType("bit");

                    b.Property<int>("IdPersona")
                        .HasColumnType("int");

                    b.HasKey("IdLegal");

                    b.HasIndex("IdPersona");

                    b.ToTable("Legal");
                });

            modelBuilder.Entity("Proyecto.Models.Persona", b =>
                {
                    b.Property<int>("IdPersona")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPersona"));

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha_Nacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("IdPersona");

                    b.ToTable("Persona");
                });

            modelBuilder.Entity("Proyecto.Models.SRI", b =>
                {
                    b.Property<int>("IdSRI")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSRI"));

                    b.Property<string>("Bienes")
                        .IsRequired()
                        .HasMaxLength(1000000000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deudas_Activas")
                        .HasColumnType("bit");

                    b.Property<int>("IdPersona")
                        .HasColumnType("int");

                    b.Property<int>("Ingresos_Mensuales")
                        .HasColumnType("int");

                    b.Property<string>("Trabajo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdSRI");

                    b.HasIndex("IdPersona");

                    b.ToTable("SRI");
                });

            modelBuilder.Entity("Proyecto.Models.Civil", b =>
                {
                    b.HasOne("Proyecto.Models.Persona", "Persona")
                        .WithMany()
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("Proyecto.Models.Legal", b =>
                {
                    b.HasOne("Proyecto.Models.Persona", "Persona")
                        .WithMany()
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("Proyecto.Models.SRI", b =>
                {
                    b.HasOne("Proyecto.Models.Persona", "Persona")
                        .WithMany()
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");
                });
#pragma warning restore 612, 618
        }
    }
}
