using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators;

public class CrearVehiculoDtoValidator : AbstractValidator<VehiculoDto>
{
    public CrearVehiculoDtoValidator()
    {
        RuleFor(x => x.Id)
            .Equal(0).When(x => x.Id != 0)
            .WithMessage("El ID debe ser 0 en creación");

        RuleFor(x => x.Marca)
            .NotEmpty().WithMessage("La marca es obligatoria");

        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("El modelo es obligatorio");

        RuleFor(x => x.Placa)
            .NotEmpty().WithMessage("La placa es obligatoria");

        RuleFor(x => x.PropietarioId)
            .GreaterThan(0).WithMessage("Debe tener propietario válido");
    }
}