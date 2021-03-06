using System.Data;
using HotelTask.Settings;
using Npgsql;

namespace HotelTask.Repositories;

public class BaseRepository
{
    private readonly IConfiguration _configuration;
    public BaseRepository(IConfiguration configuration)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        _configuration = configuration;
    }

    // new NpgsqlConnection("Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};Include Error Detail=true");
    public NpgsqlConnection NewConnection => new NpgsqlConnection(_configuration
        .GetSection(nameof(PostgresSettings)).Get<PostgresSettings>().ConnectionString);
}
