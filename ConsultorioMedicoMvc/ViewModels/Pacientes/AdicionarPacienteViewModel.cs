using System.ComponentModel.DataAnnotations;

namespace ConsultorioMedicoMvc.ViewModels.Pacientes
{
    public class AdicionarPacienteViewModel
    {
        public string CPF { get; set; } = string.Empty;

        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Data de Nascimnto")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
