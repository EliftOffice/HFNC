using System;
using System.Collections.Generic;
using HFNC_Coaches.Data.DTO;
using MySqlConnector;

namespace HFNC_Coaches.Data.DAL
{
    public class PeopleRegistryDAL
    {
        private readonly string _connectionString;

        public PeopleRegistryDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert(PeopleRegistryDTO dto)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = @"INSERT INTO people_registry 
                                (full_name, phone_number, entry_type, invited_by, current_status, system_stage, last_contact_date, notes) 
                                VALUES (@FullName, @PhoneNumber, @EntryType, @InvitedBy, @CurrentStatus, @SystemStage, @LastContactDate, @Notes)";
                
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", dto.FullName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", dto.PhoneNumber);
                    cmd.Parameters.AddWithValue("@EntryType", dto.EntryType);
                    cmd.Parameters.AddWithValue("@InvitedBy", (object)dto.InvitedBy ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CurrentStatus", dto.CurrentStatus);
                    cmd.Parameters.AddWithValue("@SystemStage", dto.SystemStage);
                    cmd.Parameters.AddWithValue("@LastContactDate", (object)dto.LastContactDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notes", (object)dto.Notes ?? DBNull.Value);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public List<PeopleRegistryDTO> GetAll()
        {
            List<PeopleRegistryDTO> list = new List<PeopleRegistryDTO>();
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT id, full_name as FullName, phone_number as PhoneNumber, entry_type as EntryType, invited_by as InvitedBy, current_status as CurrentStatus, system_stage as SystemStage, last_contact_date as LastContactDate, notes as Notes FROM people_registry ORDER BY id DESC";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map properties logic assuming you use a standard iteration or an ORM like Dapper. 
                        // For simplicity Dapper (conn.Query<PeopleRegistryDTO>) is highly recommended here, but sticking to manual reads:
                        while (reader.Read())
                        {
                            list.Add(new PeopleRegistryDTO
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                FullName = reader["FullName"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                EntryType = reader["EntryType"].ToString(),
                                InvitedBy = reader["InvitedBy"] != DBNull.Value ? reader["InvitedBy"].ToString() : null,
                                CurrentStatus = reader["CurrentStatus"].ToString(),
                                SystemStage = reader["SystemStage"].ToString(),
                                LastContactDate = reader["LastContactDate"] != DBNull.Value ? Convert.ToDateTime(reader["LastContactDate"]) : (DateTime?)null,
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}