using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BTZ_Transports.Negocio.Models.Validacao.DocumentosValidacao;

namespace BTZ_Transports.Negocio.Models.Validacao
{
    internal class MotoristaValidacao : AbstractValidator<Motorista>
    {
        public MotoristaValidacao()
        {
            {
                RuleFor(m => m.Nome)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
                RuleFor(m => m.CPF)
                   .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
                RuleFor(m => m.NumeroCNH)
                   .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
                RuleFor(m => m.CategoriaCNH)
                   .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

                RuleFor(m => m.DataDeNascimento)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");


                RuleFor(f => f.CPF.Length).Equal(CpfValidacao.TamanhoCpf)
                 .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CpfValidacao.Validar(f.CPF)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");

                RuleFor(m => m.Status)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
 
            }
        }
    }
}