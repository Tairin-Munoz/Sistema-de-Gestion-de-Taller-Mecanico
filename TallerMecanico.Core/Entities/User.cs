
using System;

namespace TallerMecanico.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!; // almacenará hash seguro
        public string Role { get; set; } = "Manager";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}