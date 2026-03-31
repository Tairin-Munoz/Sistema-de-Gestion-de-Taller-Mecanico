using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators;

public class ActualizarPropietarioDtoValidator : AbstractValidator<PropietarioDto>
{
    public ActualizarPropietarioDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombre).NotEmpty();
        RuleFor(x => x.Apellido).NotEmpty();
        RuleFor(x => x.CI).NotEmpty();
        RuleFor(x => x.Telefono).NotEmpty();
    }
}
