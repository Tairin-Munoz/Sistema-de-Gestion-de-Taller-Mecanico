namespace TallerMecanico.Core.Entities
{
    public class Tecnico : BaseEntity
    {
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public bool Activo { get; set; } = true;
    }
}
