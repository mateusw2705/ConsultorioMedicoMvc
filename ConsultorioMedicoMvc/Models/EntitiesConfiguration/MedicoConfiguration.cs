﻿using ConsultorioMedicoMvc.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsultorioMedicoMvc.Models.EntitiesConfiguration
{
    public class MedicoConfiguration: IEntityTypeConfiguration <Medico> 
    {

        public void Configure(EntityTypeBuilder<Medico> builder) 
        {
        
          builder.ToTable("Medicos");

          builder.HasKey(x => x.Id);

          builder.Property(x => x.CRM)
                 .IsRequired()
                 .HasMaxLength(20);
           
          // define a propriedade mapeada como unica
          builder.HasIndex(x => x.CRM)
                 .IsUnique();

          builder.Property(x=> x.Nome)
                 .IsRequired()
                 .HasMaxLength(200);
        }

    }
}
