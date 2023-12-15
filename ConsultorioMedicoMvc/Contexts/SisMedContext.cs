using ConsultorioMedicoMvc.Models.Entities;
using ConsultorioMedicoMvc.Models.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsultorioMedicoMvc.Contexts
{
    public class SisMedContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SisMedContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Medico> Medicos => Set<Medico>();
        public DbSet<Paciente> Pacientes => Set<Paciente>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SisMed"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MedicoConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
        }
    }
}
