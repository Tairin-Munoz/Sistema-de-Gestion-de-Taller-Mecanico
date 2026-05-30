
using FluentValidation;
using TallerMecanico.Core.DTOs;
using System.Text.RegularExpressions;

namespace TallerMecanico.Services.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("El nombre de usuario es obligatorio");
            RuleFor(x => x.Email).NotEmpty().WithMessage("El correo es obligatorio")
                                 .EmailAddress().WithMessage("Formato de correo inválido");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es obligatoria")
                                    .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");
            RuleFor(x => x.Role).NotEmpty().WithMessage("El rol es obligatorio")
                                .Must(r => r == "Manager" || r == "CEO")
                                .WithMessage("El rol debe ser 'Manager' o 'CEO'");
        }
    }
}