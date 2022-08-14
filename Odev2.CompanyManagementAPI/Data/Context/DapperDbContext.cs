using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Odev2.CompanyManagementAPI
{
    public class DapperDbContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public DapperDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = GetConnectionString();
        }

        private string GetConnectionString()
        {
            return this.configuration.GetConnectionString("PostgreSqlConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
