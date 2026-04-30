using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators;

public class CrearPropietarioDtoValidator : AbstractValidator<PropietarioDto>
{
    public CrearPropietarioDtoValidator()
    {
        RuleFor(x => x.Id)
            .Equal(0)
            .WithMessage("El ID debe ser 0 en creación");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres");

        RuleFor(x => x.Apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio");

        RuleFor(x => x.CI)
            .NotEmpty().WithMessage("El CI es obligatorio");

        RuleFor(x => x.Telefono)
            .NotEmpty().WithMessage("El teléfono es obligatorio")
            .Matches(@"^\d{8}$")
            .WithMessage("El teléfono debe tener 8 dígitos numéricos y no letras");
    }
}