using ConsultorioMedicoMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConsultorioMedicoMvc.ViewModels.Consultas
{
    public class EditarConsultaViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public TipoConsulta Tipo { get; set; }
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }
        public string NomePaciente { get; set; } = string.Empty;
        [Display(Name = "Médico")]
        public int IdMedico { get; set; }
    }
}
