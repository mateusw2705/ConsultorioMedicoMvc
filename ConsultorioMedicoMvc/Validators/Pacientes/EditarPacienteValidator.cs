using ConsultorioMedicoMvc.Contexts;
using ConsultorioMedicoMvc.ViewModels.Pacientes;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ConsultorioMedicoMvc.Validators.Pacientes
{
    public class EditarPacienteValidator : AbstractValidator<EditarPacienteViewModel>
    {
        public EditarPacienteValidator(SisMedContext context)
        {
            RuleFor(x => x.CPF).NotEmpty().WithMessage("Campo Obrigatório")
                                .Must(cpf => Regex.Replace(cpf, "[^0-9]", "").Length == 11).WithMessage("O tamanho maxímo é {MaxLength} caracteres");
                              

            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo Obrigatório")
                                .MaximumLength(200).WithMessage("O tamanho maxímo é {MaxLength} caracteres");

            RuleFor(x => x.DataNascimento).NotEmpty().WithMessage("Campo Obrigatório")
                              .Must(data => data <= DateTime.Today).WithMessage("a data de nascimento nao pode ser futura");

            RuleFor(x => x).Must(x => !context.Pacientes.Any(paciente => paciente.CPF == Regex.Replace(x.CPF, "[^0-9]", "") && paciente.Id != x.Id))
                                                        .WithMessage("Este CPF já está cadastrado.");
        }
    }
}
