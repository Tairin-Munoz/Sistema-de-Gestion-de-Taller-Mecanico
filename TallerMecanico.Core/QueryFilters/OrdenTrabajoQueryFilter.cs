namespace TallerMecanico.Core.QueryFilters
{
    public class OrdenTrabajoQueryFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? VehiculoId { get; set; }
        public int? ServicioId { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}
