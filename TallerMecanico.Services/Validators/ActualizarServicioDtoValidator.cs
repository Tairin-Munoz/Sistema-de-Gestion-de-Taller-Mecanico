using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators;

public class ActualizarServicioDtoValidator : AbstractValidator<ServicioDto>
{
    public ActualizarServicioDtoValidator()
    {
        
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID es obligatorio");

        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Precio)
            .GreaterThan(0);

        RuleFor(x => x.Activo)
            .NotNull();
    }
}