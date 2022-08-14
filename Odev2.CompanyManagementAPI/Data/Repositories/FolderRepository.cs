using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Odev2.CompanyManagementAPI
{
    public class FolderRepository
    {
        private readonly DapperDbContext dapperDbContext;

        public FolderRepository(DapperDbContext dapperDbContext)
        {
            this.dapperDbContext = dapperDbContext;
        }

        public async Task<IEnumerable<Folder>> GetAllAsync()
        {
            var query = "SELECT * FROM public.\"Folder\"";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Folder>(query);
                return result;
            }
        }

        public async Task<Folder> GetByIdAsync(int folderId)
        {
            var query = "SELECT * FROM public.\"Folder\" WHERE \"FolderId\" = @folderId";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstAsync<Folder>(query, new { folderId });
                return result;
            }
        }

        public async Task InsertAsync(Folder entity)
        {
            var query = "INSERT INTO public.\"Folder\" (\"EmpId\", \"AccessType\") VALUES (@EmpId, @AccessType)";

            var parameters = new DynamicParameters();
            parameters.Add("EmpId", entity.EmpId, DbType.Int32);
            parameters.Add("AccessType", entity.AccessType, DbType.String);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateAsync(Folder entity)
        {
            var query = "UPDATE public.\"Folder\"" +
                " SET \"EmpId\"=@EmpId, \"AccessType\"=@AccessType" +
                " WHERE \"FolderId\" = @FolderId";

            var parameters = new DynamicParameters();
            parameters.Add("EmpId", entity.EmpId, DbType.Int32);
            parameters.Add("AccessType", entity.AccessType, DbType.String);
            parameters.Add("FolderId", entity.FolderId, DbType.Int32);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task RemoveAsync(int folderId)
        {
            var query = "DELETE FROM public.\"Folder\" WHERE \"FolderId\" = @folderId";

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, new { folderId });
            }
        }
    }
}
