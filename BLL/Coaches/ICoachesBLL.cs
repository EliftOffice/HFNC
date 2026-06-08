using HFNC_Coaches.Data.DTO.Coaches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HFNC_Coaches.BLL.Coaches
{
    public interface ICoachesBLL
    {
        Task<IEnumerable<CoachResponseDTO>> GetAllCoachesAsync();
        Task<CoachResponseDTO?> GetCoachByIdAsync(int id);
        Task<CoachResponseDTO> CreateCoachAsync(CreateCoachDTO coach);
    }
}
