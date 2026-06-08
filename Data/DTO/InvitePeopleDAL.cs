using System;
using System.Collections.Generic;
using HFNC_Coaches.Data.DTO;
using MySqlConnector;

namespace HFNC_Coaches.Data.DAL
{
    public class InvitePeopleDAL
    {
        private readonly string _connectionString;

        public InvitePeopleDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<InvitePeopleDTO> GetAll()
        {
            List<InvitePeopleDTO> list = new List<InvitePeopleDTO>();
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT id, name FROM invite_people ORDER BY name ASC";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new InvitePeopleDTO
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = reader["name"].ToString() ?? string.Empty
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}