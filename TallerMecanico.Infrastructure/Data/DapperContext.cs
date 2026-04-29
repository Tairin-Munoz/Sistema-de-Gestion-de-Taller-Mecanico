using System.Data;
using Dapper;
using TallerMecanico.Core.Interfaces;

namespace TallerMecanico.Infrastructure.Data;

public class DapperContext : IDapperContext
{
    private readonly IDbConnectionFactory _factory;
    private IDbConnection? _connection;
    private IDbTransaction? _transaction;

    public DapperContext(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public void SetAmbientConnection(IDbConnection connection, IDbTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    public void ClearAmbientConnection()
    {
        _connection = null;
        _transaction = null;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
    {
        var conn = _connection ?? _factory.CreateConnection();
        return await conn.QueryAsync<T>(sql, param, _transaction);
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null)
    {
        var conn = _connection ?? _factory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<T>(sql, param, _transaction);
    }
}