using System.Data;
using DevQuestions.Application.Database;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DevQuestions.Infrastructure.Postgres;

public class SqlConnection(IConfiguration configuration) : ISqlConnection
{
    private readonly IConfiguration _configuration = configuration;

    public IDbConnection GetConnection() => new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
}