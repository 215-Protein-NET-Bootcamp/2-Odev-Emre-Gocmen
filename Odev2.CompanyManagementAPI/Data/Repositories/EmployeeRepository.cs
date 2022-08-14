using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Odev2.CompanyManagementAPI
{
    public class EmployeeRepository
    {
        private readonly DapperDbContext dapperDbContext;

        public EmployeeRepository(DapperDbContext dapperDbContext)
        {
            this.dapperDbContext = dapperDbContext;
        }

        public async Task<EmployeeDto> GetByIdWithDetailsAsync(int employeeId)
        {
 //   SELECT "Employee"."EmpId", "EmpName", "FolderId", "AccessType", "DeptName", "CountryName", "Continent", "Currency"
 //   FROM public."Employee", public."Folder", public."Department", public."Country"
 //   WHERE public."Employee"."EmpId"=public."Folder"."EmpId" 
 //   AND public."Department"."DeptId"=public."Employee"."DeptId" 
 //   AND public."Country"."CountryId"=public."Department"."CountryId"
 //   AND public."Employee"."EmpId"= 7            
            var query = "SELECT public.\"Employee\".\"EmpId\", \"EmpName\", \"FolderId\", \"AccessType\", \"DeptName\", " +
                "\"CountryName\", \"Continent\", \"Currency\"" +
                " FROM public.\"Employee\", public.\"Folder\", public.\"Department\", public.\"Country\"" +
                " WHERE public.\"Employee\".\"EmpId\" = public.\"Folder\".\"EmpId\"" +
                " AND public.\"Department\".\"DeptId\" = public.\"Employee\".\"DeptId\"" +
                " AND public.\"Country\".\"CountryId\" = public.\"Department\".\"CountryId\"" +
                " AND public.\"Employee\".\"EmpId\" = @employeeId";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstAsync<EmployeeDto>(query, new { employeeId });
                return result;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var query = "SELECT * FROM public.\"Employee\"";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Employee>(query);
                return result;
            }
        }

        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            var query = "SELECT * FROM public.\"Employee\" WHERE \"EmpId\" = @employeeId";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstAsync<Employee>(query, new { employeeId });
                return result;
            }
        }

        public async Task InsertAsync(Employee entity)
        {
            var query = "INSERT INTO public.\"Employee\" (\"EmpName\", \"DeptId\") VALUES (@EmpName, @DeptId)";

            var parameters = new DynamicParameters();
            parameters.Add("EmpName", entity.EmpName, DbType.String);
            parameters.Add("DeptId", entity.DeptId, DbType.Int32);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateAsync(Employee entity)
        {
            var query = "UPDATE public.\"Employee\"" +
                " SET \"EmpName\"=@EmpName, \"DeptId\"=@DeptId" +
                " WHERE \"EmpId\" = @EmpId";

            var parameters = new DynamicParameters();
            parameters.Add("EmpName", entity.EmpName, DbType.String);
            parameters.Add("DeptId", entity.DeptId, DbType.Int32);
            parameters.Add("EmpId", entity.EmpId, DbType.Int32);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task RemoveAsync(int employeeId)
        {
            var query = "DELETE FROM \"Employee\" WHERE \"EmpId\" = @employeeId";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, new { employeeId });
            }
        }
    }
}
