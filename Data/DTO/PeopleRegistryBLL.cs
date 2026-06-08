using System;
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

        public PeopleRegistryDTO? GetPersonById(int id)
        {
            try
            {
                return _dal.GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BLL GetPersonById Error: {ex.Message}");
                throw;
            }
        }

        public bool UpdatePerson(PeopleRegistryDTO person)
        {
            try
            {
                return _dal.Update(person) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BLL UpdatePerson Error: {ex.Message}");
                throw;
            }
        }

        public bool DeletePerson(int id)
        {
            try
            {
                return _dal.Delete(id) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BLL DeletePerson Error: {ex.Message}");
                throw;
            }
        }
    }
}