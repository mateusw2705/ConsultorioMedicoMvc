using ConsultorioMedicoMvc.Contexts;
using ConsultorioMedicoMvc.ViewModels.Pacientes;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ConsultorioMedicoMvc.Validators.Pacientes
{
    public class AdicionarPacienteValidator : AbstractValidator<AdicionarPacienteViewModel>
    {
        public AdicionarPacienteValidator(SisMedContext context)
        {
            RuleFor(x => x.CPF).NotEmpty().WithMessage("Campo Obrigatório")
                                .Must(cpf => Regex.Replace(cpf, "[^0-9]", "").Length == 11).WithMessage("O tamanho maxímo é {MaxLength} caracteres")
                               .Must(cpf => !context.Pacientes.Any(p => p.CPF == cpf)).WithMessage("Este CPF ja está em uso");

            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo Obrigatório")
                                .MaximumLength(200).WithMessage("O tamanho maxímo é {MaxLength} caracteres");

            RuleFor(x => x.DataNascimento).NotEmpty().WithMessage("Campo Obrigatório")
                              .Must(data=> data <= DateTime.Today).WithMessage("a data de nascimento nao pode ser futura");
        }
    }
}
