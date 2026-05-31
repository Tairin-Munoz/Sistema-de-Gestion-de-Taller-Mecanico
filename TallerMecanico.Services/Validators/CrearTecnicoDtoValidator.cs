using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators;

public class CrearTecnicoDtoValidator : AbstractValidator<TecnicoDto>
{
    public CrearTecnicoDtoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$").WithMessage("El nombre solo debe contener letras y espacios");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo es obligatorio")
            .EmailAddress().WithMessage("Formato de correo inválido");

        RuleFor(x => x.Telefono)
            .NotEmpty().WithMessage("El teléfono es obligatorio")
            .Matches("^[0-9]+$").WithMessage("El teléfono solo debe contener números")
            .MinimumLength(7).WithMessage("El teléfono debe tener al menos 7 dígitos");
    }
}
