namespace TallerMecanico.Core.DTOs
{
    public class HistorialDto
    {
        public int Id { get; set; }
        public int PropietarioId { get; set; }
        public string PropietarioNombre { get; set; } = null!;
        public int VehiculoId { get; set; }
        public string Placa { get; set; } = null!;
        public int ServicioId { get; set; }
        public string ServicioNombre { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = null!;
        public decimal Precio { get; set; }
    }
}
