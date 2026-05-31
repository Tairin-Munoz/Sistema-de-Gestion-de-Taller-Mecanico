namespace TallerMecanico.Core.QueryFilters
{
    public class ServicioQueryFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Nombre { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public bool? Activo { get; set; }
    }
}
