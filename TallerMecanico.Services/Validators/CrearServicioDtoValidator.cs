using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators;

public class CrearServicioDtoValidator : AbstractValidator<ServicioDto>
{
    public CrearServicioDtoValidator()
    {
        
        RuleFor(x => x.Id)
            .Equal(0).When(x => x.Id != 0)
            .WithMessage("El ID debe ser 0 en creación");

        
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MinimumLength(3).WithMessage("Debe tener al menos 3 caracteres");

        
        RuleFor(x => x.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

        
        RuleFor(x => x.Activo)
            .NotNull().WithMessage("El estado activo es obligatorio");
    }
}