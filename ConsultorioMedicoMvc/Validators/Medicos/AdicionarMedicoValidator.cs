using ConsultorioMedicoMvc.Contexts;
using ConsultorioMedicoMvc.ViewModels.Medicos;
using FluentValidation;

namespace ConsultorioMedicoMvc.Validators.Medicos
{
    public class AdicionarMedicoValidator : AbstractValidator<AdicionarMedicoViewModel>
    {
        public AdicionarMedicoValidator(SisMedContext context)
        {
            RuleFor(x => x.CRM).NotEmpty().WithMessage("Campo Obrigatório")
                               .MaximumLength(20).WithMessage("O tamanho maxímo é {MaxLength} caracteres")
                               .Must(crm => !context.Medicos.Any(m => m.CRM == crm)).WithMessage("Este CRM ja está em uso");

            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo Obrigatório")
                                .MaximumLength(200).WithMessage("O tamanho maxímo é {MaxLength} caracteres");
        }
    }
}
