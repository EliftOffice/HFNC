using HFNC_Coaches.DAL.Coaches;
using HFNC_Coaches.Data.DTO.Coaches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HFNC_Coaches.BLL.Coaches
{
    public class CoachesBLL : ICoachesBLL
    {
        private readonly ICoachesDAL _coachesDAL;

        public CoachesBLL(ICoachesDAL coachesDAL)
        {
            _coachesDAL = coachesDAL;
        }

        public Task<CoachResponseDTO> CreateCoachAsync(CreateCoachDTO coach)
        {
            return _coachesDAL.CreateCoachAsync(coach);
        }

        public Task<IEnumerable<CoachResponseDTO>> GetAllCoachesAsync()
        {
            return _coachesDAL.GetAllCoachesAsync();
        }

        public Task<CoachResponseDTO?> GetCoachByIdAsync(int id)
        {
            return _coachesDAL.GetCoachByIdAsync(id);
        }
    }
}
