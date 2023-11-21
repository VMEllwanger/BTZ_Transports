using FluentValidation;

namespace BTZ_Transports.Negocio.Models.Validacao
{
    public class VeiculoValidacao : AbstractValidator<Veiculo>
    {
        public VeiculoValidacao()
        {
            {
                RuleFor(c => c.Nome)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
                RuleFor(c => c.CombustivelId)
                   .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
                RuleFor(c => c.Fabricante)
                   .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
                RuleFor(c => c.AnoDeFabricacao)
                   .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");


                RuleFor(c => c.CapacidadeMaximaDoTanque)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

                RuleFor(c => c.CapacidadeMaximaDoTanque)
                    .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
            }
        }
    }
}
