using System.Collections.Generic;
using HFNC_Coaches.Data.DAL;
using HFNC_Coaches.Data.DTO;

namespace HFNC_Coaches.Business.BLL
{
    public class InvitePeopleBLL
    {
        private readonly InvitePeopleDAL _dal;

        public InvitePeopleBLL(string connectionString)
        {
            _dal = new InvitePeopleDAL(connectionString);
        }

        public List<InvitePeopleDTO> GetAllPeople()
        {
            return _dal.GetAll();
        }
    }
}