namespace TallerMecanico.Infrastructure.Queries
{
    public static class OrdenTrabajoQuery
    {
        public static string GetAll = @"
            SELECT 
                Id,
                VehiculoId,
                ServicioId,
                Fecha,
                Estado
            FROM OrdenesTrabajo";
    }
}