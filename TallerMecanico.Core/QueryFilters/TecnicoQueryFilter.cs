namespace TallerMecanico.Core.QueryFilters
{
    public class TecnicoQueryFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Nombre { get; set; }
        public string? Email { get; set; }
    }
}
