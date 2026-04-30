namespace TallerMecanico.Core.DTOs
{
    public class OrdenTrabajoDto
    {
        public int Id { get; set; }
        public int VehiculoId { get; set; }
        public int ServicioId { get; set; }
        public string Fecha { get; set; }
        public string Estado { get; set; }
    }
}