using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators;

public class VehiculoDtoValidator : AbstractValidator<VehiculoDto>
{
    public VehiculoDtoValidator()
    {
        RuleFor(x => x.Marca)
            .NotEmpty().WithMessage("La marca es obligatoria");

        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("El modelo es obligatorio");

        RuleFor(x => x.Placa)
            .NotEmpty().WithMessage("La placa es obligatoria")
            .MinimumLength(5).WithMessage("La placa debe tener al menos 5 caracteres");

        RuleFor(x => x.PropietarioId)
            .GreaterThan(0).WithMessage("Debe tener un propietario válido");
    }
}