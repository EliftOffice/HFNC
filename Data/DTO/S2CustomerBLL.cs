using System;
using HFNC_Coaches.Data.DAL;
using HFNC_Coaches.Data.DTO;

namespace HFNC_Coaches.Business.BLL
{
    public class S2CustomerBLL
    {
        private readonly S2CustomerDAL _dal;

        public S2CustomerBLL(string connectionString)
        {
            _dal = new S2CustomerDAL(connectionString);
        }

        public bool AddCustomer(S2CustomerDTO customer)
        {
            try
            {
                return _dal.Insert(customer) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BLL S2Customer Error: {ex.Message}");
                throw;
            }
        }

        public int GetActiveCustomersLast30Days()
        {
            return _dal.GetActiveCustomersLast30DaysCount();
        }
    }
}