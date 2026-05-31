
using System;

namespace TallerMecanico.Core.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!; // almacenar� hash seguro
        public string Role { get; set; } = "Manager";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}