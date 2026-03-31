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
            .GreaterThan(0)
            .WithMessage("El ID es obligatorio para actualizar");

        RuleFor(x => x.Marca)
            .NotEmpty().WithMessage("La marca es obligatoria");

        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("El modelo es obligatorio");

        RuleFor(x => x.Placa)
            .NotEmpty().WithMessage("La placa es obligatoria");

        RuleFor(x => x.PropietarioId)
            .GreaterThan(0)
            .WithMessage("Debe tener propietario válido");
    }
}