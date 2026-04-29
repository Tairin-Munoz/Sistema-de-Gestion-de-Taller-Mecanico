using FluentValidation;
using TallerMecanico.Core.DTOs;

public class CrearOrdenTrabajoDtoValidator : AbstractValidator<OrdenTrabajoDto>
{
    public CrearOrdenTrabajoDtoValidator()
    {
        RuleFor(x => x.Id)
            .Equal(0).When(x => x.Id != 0);

        RuleFor(x => x.VehiculoId)
            .GreaterThan(0);

        RuleFor(x => x.ServicioId)
            .GreaterThan(0);

        RuleFor(x => x.Fecha)
            .NotEmpty();

        RuleFor(x => x.Estado)
            .NotEmpty()
            .MinimumLength(3);
    }
}