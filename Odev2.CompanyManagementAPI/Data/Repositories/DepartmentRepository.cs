using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Odev2.CompanyManagementAPI
{
    public class DepartmentRepository
    {
        private readonly DapperDbContext dapperDbContext;

        public DepartmentRepository(DapperDbContext dapperDbContext)
        {
            this.dapperDbContext = dapperDbContext;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var query = "SELECT * FROM public.\"Department\"";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Department>(query);
                return result;
            }
        }

        public async Task<Department> GetByIdAsync(int departmentId)
        {
            var query = "SELECT * FROM public.\"Department\" WHERE \"DeptId\" = @departmentId";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstAsync<Department>(query, new { departmentId });
                return result;
            }
        }

        public async Task InsertAsync(Department entity)
        {
            var query = "INSERT INTO public.\"Department\" (\"DeptName\", \"CountryId\") VALUES (@DeptName, @CountryId)";

            var parameters = new DynamicParameters();
            parameters.Add("DeptName", entity.DeptName, DbType.String);
            parameters.Add("CountryId", entity.CountryId, DbType.Int32);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateAsync(Department entity)
        {
            var query = "UPDATE public.\"Department\"" +
                " SET \"DeptName\"=@DeptName, \"CountryId\"=@CountryId" +
                " WHERE \"DeptId\"=@DeptId";

            var parameters = new DynamicParameters();
            parameters.Add("DeptName", entity.DeptName, DbType.String);
            parameters.Add("CountryId", entity.CountryId, DbType.Int32);
            parameters.Add("DeptId", entity.DeptId, DbType.Int32);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task RemoveAsync(int departmentId)
        {
            var query = "DELETE FROM \"Department\" WHERE \"DeptId\" = @departmentId";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, new { departmentId });
            }
        }
    }
}
