using System.Collections.Generic;
using HFNC_Coaches.Data.DAL;
using HFNC_Coaches.Data.DTO;

namespace HFNC_Coaches.Business.BLL
{
    public class ProductsBLL
    {
        private readonly ProductsDAL _dal;

        public ProductsBLL(string connectionString)
        {
            _dal = new ProductsDAL(connectionString);
        }

        public List<ProductsDTO> GetAllProducts()
        {
            return _dal.GetAll();
        }
    }
}