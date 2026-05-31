namespace TallerMecanico.Core.DTOs
{
    public class TecnicoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public bool Activo { get; set; } = true;
    }
}
