using System.Collections.Generic;
using HFNC_Coaches.Data.DAL;
using HFNC_Coaches.Data.DTO;

namespace HFNC_Coaches.Business.BLL
{
    public class PeopleRegistryBLL
    {
        private readonly PeopleRegistryDAL _dal;

        public PeopleRegistryBLL(string connectionString)
        {
            _dal = new PeopleRegistryDAL(connectionString);
        }

        public bool AddPerson(PeopleRegistryDTO person)
        {
            return _dal.Insert(person) > 0;
        }

        public List<PeopleRegistryDTO> GetAllPeople()
        {
            return _dal.GetAll();
        }
    }
}