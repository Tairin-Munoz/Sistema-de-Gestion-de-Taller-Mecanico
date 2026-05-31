namespace TallerMecanico.Core.QueryFilters
{
    public class HistorialQueryFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? VehiculoId { get; set; }
        public string? Placa { get; set; }
        public int? PropietarioId { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}
