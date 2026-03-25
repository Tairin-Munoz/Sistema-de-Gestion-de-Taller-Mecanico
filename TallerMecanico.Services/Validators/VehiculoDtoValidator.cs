using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators;

public class VehiculoDtoValidator : AbstractValidator<VehiculoDto>
{
    public VehiculoDtoValidator()
    {
        RuleFor(x => x.Placa).NotEmpty();
        RuleFor(x => x.Marca).NotEmpty();
        RuleFor(x => x.Modelo).NotEmpty();
        RuleFor(x => x.Anio).GreaterThan(1900);
    }
}
