using Dapper;
using HFNC_Coaches.Data.DTO.Coaches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HFNC_Coaches.DAL.Coaches
{
    public class CoachesDAL : ICoachesDAL
    {
        private readonly HFNC_Coaches.Data.IDbConnectionFactory _connectionFactory;

        public CoachesDAL(HFNC_Coaches.Data.IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<CoachResponseDTO>> GetAllCoachesAsync()
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = "SELECT id AS Id, name AS Name, specialization AS Specialization, email AS Email FROM coaches";
            return await connection.QueryAsync<CoachResponseDTO>(sql);
        }

        public async Task<CoachResponseDTO?> GetCoachByIdAsync(int id)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = "SELECT id AS Id, name AS Name, specialization AS Specialization, email AS Email FROM coaches WHERE id = @Id";
            return await connection.QuerySingleOrDefaultAsync<CoachResponseDTO>(sql, new { Id = id });
        }

        public async Task<CoachResponseDTO> CreateCoachAsync(CreateCoachDTO coach)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"
                INSERT INTO coaches (name, specialization, email)
                VALUES (@Name, @Specialization, @Email);
                SELECT LAST_INSERT_ID();";

            var id = await connection.QuerySingleAsync<int>(sql, coach);
            return new CoachResponseDTO
            {
                Id = id,
                Name = coach.Name,
                Specialization = coach.Specialization,
                Email = coach.Email
            };
        }
    }
}
