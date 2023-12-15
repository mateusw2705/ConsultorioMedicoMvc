using ConsultorioMedicoMvc.Contexts;
using ConsultorioMedicoMvc.ViewModels.Medicos;
using FluentValidation;

namespace ConsultorioMedicoMvc.Validators.Medicos
{
    public class EditarMedicoValidator : AbstractValidator<EditarMedicoViewModel>
    {
        public EditarMedicoValidator(SisMedContext context)
        {
            RuleFor(x => x.CRM).NotEmpty().WithMessage("Campo Obrigatório")
                               .MaximumLength(20).WithMessage("O tamanho maxímo é {MaxLength} caracteres");

            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo Obrigatório")
                                .MaximumLength(200).WithMessage("O tamanho maxímo é {MaxLength} caracteres");

            RuleFor(medico => medico).Must(medico => !context.Medicos.Any(med => med.CRM == medico.CRM && medico.Id != med.Id))
                                     .WithMessage("O crm ja esta em uso");
        }
    }
}
