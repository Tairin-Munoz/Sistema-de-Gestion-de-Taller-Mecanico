using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators;

public class ActualizarVehiculoDtoValidator : AbstractValidator<VehiculoDto>
{
    public ActualizarVehiculoDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Marca)
            .NotEmpty();

        RuleFor(x => x.Modelo)
            .NotEmpty();

        RuleFor(x => x.Placa)
            .NotEmpty();

        RuleFor(x => x.PropietarioId)
            .GreaterThan(0);
    }
}