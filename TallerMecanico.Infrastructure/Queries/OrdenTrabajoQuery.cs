namespace TallerMecanico.Infrastructure.Queries;

public static class OrdenTrabajoQuery
{
    public static string GetAll = @"
        SELECT 
            ot.Id,
            ot.VehiculoId,
            ot.ServicioId,
            ot.Fecha,
            ot.Estado
        FROM OrdenTrabajo ot";
}