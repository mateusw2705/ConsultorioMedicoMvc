﻿// <auto-generated />
using System;
using ConsultorioMedicoMvc.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConsultorioMedicoMvc.Migrations
{
    [DbContext(typeof(SisMedContext))]
    [Migration("20231215130254_EntidadeInfoComplementares")]
    partial class EntidadeInfoComplementares
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConsultorioMedicoMvc.Models.Entities.InformacoesComplementaresPaciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alergias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CirurgiasRealizadas")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int");

                    b.Property<string>("MedicamentosEmUso")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdPaciente")
                        .IsUnique();

                    b.ToTable("InformacoesComplementaresPaciente");
                });

            modelBuilder.Entity("ConsultorioMedicoMvc.Models.Entities.Medico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CRM")
                        .IsUnique();

                    b.ToTable("Medicos", (string)null);
                });

            modelBuilder.Entity("ConsultorioMedicoMvc.Models.Entities.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.ToTable("Pacientes", (string)null);
                });

            modelBuilder.Entity("ConsultorioMedicoMvc.Models.Entities.InformacoesComplementaresPaciente", b =>
                {
                    b.HasOne("ConsultorioMedicoMvc.Models.Entities.Paciente", "Paciente")
                        .WithOne("InformacoesComplementares")
                        .HasForeignKey("ConsultorioMedicoMvc.Models.Entities.InformacoesComplementaresPaciente", "IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("ConsultorioMedicoMvc.Models.Entities.Paciente", b =>
                {
                    b.Navigation("InformacoesComplementares");
                });
#pragma warning restore 612, 618
        }
    }
}
