using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators;

public class CrearPropietarioDtoValidator : AbstractValidator<PropietarioDto>
{
    public CrearPropietarioDtoValidator()
    {
        RuleFor(x => x.Id).Equal(0);
        RuleFor(x => x.Nombre).NotEmpty();
        RuleFor(x => x.Apellido).NotEmpty();
        RuleFor(x => x.CI).NotEmpty();
        RuleFor(x => x.Telefono).NotEmpty();
    }
}