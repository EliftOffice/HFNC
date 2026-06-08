using System;
using System.Collections.Generic;
using HFNC_Coaches.Data.DTO;
using MySqlConnector;
using System.Data;
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
            try
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
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"DAL Insert Error: {ex.Message}");
                throw;
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
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PeopleRegistryDTO
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                FullName = reader["FullName"].ToString() ?? string.Empty,
                                PhoneNumber = reader["PhoneNumber"].ToString() ?? string.Empty,
                                EntryType = reader["EntryType"].ToString() ?? string.Empty,
                                InvitedBy = reader["InvitedBy"] != DBNull.Value ? reader["InvitedBy"].ToString() : null,
                                CurrentStatus = reader["CurrentStatus"].ToString() ?? string.Empty,
                                SystemStage = reader["SystemStage"].ToString() ?? string.Empty,
                                LastContactDate = reader["LastContactDate"] != DBNull.Value ? Convert.ToDateTime(reader["LastContactDate"]) : (DateTime?)null,
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null
                            });
                        }
                    }
                }
            }
            return list;
        }

        public PeopleRegistryDTO? GetById(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    string query = "SELECT id, full_name as FullName, phone_number as PhoneNumber, entry_type as EntryType, invited_by as InvitedBy, current_status as CurrentStatus, system_stage as SystemStage, last_contact_date as LastContactDate, notes as Notes FROM people_registry WHERE id = @Id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new PeopleRegistryDTO
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    FullName = reader["FullName"].ToString() ?? string.Empty,
                                    PhoneNumber = reader["PhoneNumber"].ToString() ?? string.Empty,
                                    EntryType = reader["EntryType"].ToString() ?? string.Empty,
                                    InvitedBy = reader["InvitedBy"] != DBNull.Value ? reader["InvitedBy"].ToString() : null,
                                    CurrentStatus = reader["CurrentStatus"].ToString() ?? string.Empty,
                                    SystemStage = reader["SystemStage"].ToString() ?? string.Empty,
                                    LastContactDate = reader["LastContactDate"] != DBNull.Value ? Convert.ToDateTime(reader["LastContactDate"]) : null,
                                    Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DAL GetById Error: {ex.Message}");
                throw;
            }
            return null;
        }

        public int Update(PeopleRegistryDTO dto)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    string query = @"UPDATE people_registry SET 
                                        full_name = @FullName, 
                                        phone_number = @PhoneNumber, 
                                        entry_type = @EntryType, 
                                        invited_by = @InvitedBy, 
                                        current_status = @CurrentStatus, 
                                        system_stage = @SystemStage, 
                                        last_contact_date = @LastContactDate, 
                                        notes = @Notes 
                                    WHERE id = @Id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", dto.Id);
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
            catch (Exception ex)
            {
                Console.WriteLine($"DAL Update Error: {ex.Message}");
                throw;
            }
        }

        public int Delete(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    string query = "DELETE FROM people_registry WHERE id = @Id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DAL Delete Error: {ex.Message}");
                throw;
            }
        }
    }
}