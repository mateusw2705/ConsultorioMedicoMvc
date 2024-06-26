﻿using ConsultorioMedicoMvc.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioMedicoMvc.Models.EntitiesConfiguration
{
    public class MonitoramentoPacienteConfiguration : IEntityTypeConfiguration<MonitoramentoPaciente>
    {
        public void Configure(EntityTypeBuilder<MonitoramentoPaciente> builder)
        {
            builder.ToTable("MonitoramentoPaciente");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PressaoArterial)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(x => x.Temperatura)
                   .HasColumnType("DECIMAL(3,1)");

            builder.Property(x => x.SaturacaoOxigenio)
                   .HasColumnType("TINYINT");

            builder.Property(x => x.FrequenciaCardiaca)
                   .HasColumnType("TINYINT");

            builder.Property(x => x.DataAfericao)
                   .IsRequired();
        }
    }
}
