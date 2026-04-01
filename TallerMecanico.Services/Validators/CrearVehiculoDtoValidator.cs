using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators;

public class CrearVehiculoDtoValidator : AbstractValidator<VehiculoDto>
{
    public CrearVehiculoDtoValidator()
    {
        RuleFor(x => x.Id)
            .Equal(0)
            .WithMessage("El ID debe ser 0 en creación");

        RuleFor(x => x.Marca)
            .NotEmpty().WithMessage("La marca es obligatoria")
            .MinimumLength(2).WithMessage("La marca debe tener al menos 2 caracteres");

        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("El modelo es obligatorio")
            .MinimumLength(2).WithMessage("El modelo debe tener al menos 2 caracteres");

        RuleFor(x => x.Placa)
            .NotEmpty().WithMessage("La placa es obligatoria")
            .MinimumLength(5).WithMessage("La placa debe tener al menos 5 caracteres");

        RuleFor(x => x.PropietarioId)
            .GreaterThan(0).WithMessage("El propietario es obligatorio");
    }
}