namespace TallerMecanico.Core.QueryFilters
{
    public class VehiculoQueryFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Placa { get; set; }
        public int? PropietarioId { get; set; }
    }
}
