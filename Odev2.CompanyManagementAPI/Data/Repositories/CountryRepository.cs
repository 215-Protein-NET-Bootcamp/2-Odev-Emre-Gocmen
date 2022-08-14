using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Odev2.CompanyManagementAPI
{
    public class CountryRepository
    {
        private readonly DapperDbContext dapperDbContext;

        public CountryRepository(DapperDbContext dapperDbContext)
        {
            this.dapperDbContext = dapperDbContext;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            var query = "SELECT * FROM public.\"Country\"";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Country>(query);
                return result;
            }
        }

        public async Task<Country> GetByIdAsync(int countryId)
        {
            var query = "SELECT * FROM public.\"Country\" WHERE \"CountryId\" = @countryId";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstAsync<Country>(query, new { countryId });
                return result;
            }
        }

        public async Task InsertAsync(Country entity)
        {
            var query = "INSERT INTO public.\"Country\" (\"CountryName\", \"Continent\", \"Currency\") VALUES (@CountryName, @Continent, @Currency)";

            var parameters = new DynamicParameters();
            parameters.Add("CountryName", entity.CountryName, DbType.String);
            parameters.Add("Continent", entity.Continent, DbType.String);
            parameters.Add("Currency", entity.Currency, DbType.String);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateAsync(Country entity)
        {
            var query = "UPDATE public.\"Country\"" +
                " SET \"CountryName\"=@CountryName, \"Continent\"=@Continent, \"Currency\"=@Currency" +
                " WHERE \"CountryId\"=@CountryId";

            var parameters = new DynamicParameters();
            parameters.Add("CountryName", entity.CountryName, DbType.String);
            parameters.Add("Continent", entity.Continent, DbType.String);
            parameters.Add("Currency", entity.Currency, DbType.String);
            parameters.Add("CountryId", entity.CountryId, DbType.Int32);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task RemoveAsync(int countryId)
        {
            var query = "DELETE FROM \"Country\" WHERE \"CountryId\" = @countryId";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, new { countryId });
            }
        }
    }
}
