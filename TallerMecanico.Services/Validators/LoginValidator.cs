
using FluentValidation;
using TallerMecanico.Core.DTOs;

namespace TallerMecanico.Services.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("El nombre de usuario es obligatorio");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es obligatoria")
                                    .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");
        }
    }
}