using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using TallerMecanico.Core.Interfaces;

namespace TallerMecanico.Infrastructure.Data;

public class DapperContext : IDapperContext
{
    private readonly IConfiguration _config;
    private IDbConnection _connection;
    private IDbTransaction _transaction;

    public DapperContext(IConfiguration config)
    {
        _config = config;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
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

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        if (_connection != null)
            return await _connection.QueryAsync<T>(sql, parameters, transaction: _transaction);

        using var connection = CreateConnection();
        return await connection.QueryAsync<T>(sql, parameters);
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null)
    {
        if (_connection != null)
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, parameters, transaction: _transaction);

        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
    }
}