using HFNC_Coaches.Data.DTO.Coaches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HFNC_Coaches.DAL.Coaches
{
    public interface ICoachesDAL
    {
        Task<IEnumerable<CoachResponseDTO>> GetAllCoachesAsync();
        Task<CoachResponseDTO?> GetCoachByIdAsync(int id);
        Task<CoachResponseDTO> CreateCoachAsync(CreateCoachDTO coach);
    }
}
