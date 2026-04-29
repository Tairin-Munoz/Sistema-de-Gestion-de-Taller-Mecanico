namespace TallerMecanico.Infrastructure.Queries;

public static class ServicioQuery
{
    public static string GetAll = @"
        SELECT Id, Nombre, Precio, Activo
        FROM Servicios";
}