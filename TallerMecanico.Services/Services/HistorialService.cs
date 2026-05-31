using Dapper;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Core.QueryFilters;
using TallerMecanico.Services.Interfaces;

namespace TallerMecanico.Services.Services;

public class HistorialService : IHistorialService
{
    private readonly IDapperContext _dapper;

    public HistorialService(IDapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<HistorialDto>> GetHistorialAsync(HistorialQueryFilter filter)
    {
        var sql = @"
            SELECT
                ot.Id,
                p.Id AS PropietarioId,
                CONCAT(p.Nombre, ' ', p.Apellido) AS PropietarioNombre,
                v.Id AS VehiculoId,
                v.Placa,
                s.Id AS ServicioId,
                s.Nombre AS ServicioNombre,
                ot.Fecha,
                ot.Estado,
                s.Precio
            FROM OrdenesTrabajo ot
            INNER JOIN vehiculo v ON ot.VehiculoId = v.Id
            INNER JOIN propietario p ON v.PropietarioId = p.Id
            INNER JOIN Servicios s ON ot.ServicioId = s.Id
            WHERE 1 = 1";

        var parameters = new DynamicParameters();

        if (filter.VehiculoId.HasValue)
        {
            sql += " AND v.Id = @VehiculoId";
            parameters.Add("VehiculoId", filter.VehiculoId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Placa))
        {
            sql += " AND v.Placa LIKE @Placa";
            parameters.Add("Placa", $"%{filter.Placa}%");
        }

        if (filter.PropietarioId.HasValue)
        {
            sql += " AND p.Id = @PropietarioId";
            parameters.Add("PropietarioId", filter.PropietarioId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Estado))
        {
            sql += " AND ot.Estado LIKE @Estado";
            parameters.Add("Estado", $"%{filter.Estado}%");
        }

        if (filter.FechaDesde.HasValue)
        {
            sql += " AND ot.Fecha >= @FechaDesde";
            parameters.Add("FechaDesde", filter.FechaDesde.Value.Date);
        }

        if (filter.FechaHasta.HasValue)
        {
            sql += " AND ot.Fecha <= @FechaHasta";
            parameters.Add("FechaHasta", filter.FechaHasta.Value.Date.AddDays(1).AddTicks(-1));
        }

        var result = await _dapper.QueryAsync<HistorialDto>(sql, parameters);
        return result;
    }
}