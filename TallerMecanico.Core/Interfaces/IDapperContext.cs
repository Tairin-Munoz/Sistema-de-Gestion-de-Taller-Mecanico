using System.Data;

namespace TallerMecanico.Core.Interfaces;

public interface IDapperContext
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);
    Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null);

    void SetAmbientConnection(IDbConnection connection, IDbTransaction transaction);
    void ClearAmbientConnection();
}