using System;
using System.Collections.Generic;
using HFNC_Coaches.Data.DTO;
using MySqlConnector;

namespace HFNC_Coaches.Data.DAL
{
    public class ProductsDAL
    {
        private readonly string _connectionString;

        public ProductsDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ProductsDTO> GetAll()
        {
            List<ProductsDTO> list = new List<ProductsDTO>();
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT id, product_name FROM products ORDER BY product_name ASC";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ProductsDTO
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                ProductName = reader["product_name"].ToString() ?? string.Empty
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}