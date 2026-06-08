using System;
using MySqlConnector;
using HFNC_Coaches.Data.DTO;

namespace HFNC_Coaches.Data.DAL
{
    public class S2CustomerDAL
    {
        private readonly string _connectionString;

        public S2CustomerDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert(S2CustomerDTO dto)
        {
            try
            {
                // Business rules logic: Auto-calculate tracking dates
                DateTime lastPurchaseDate = dto.FirstPurchaseDate;
                DateTime reorderDueDate = lastPurchaseDate.AddDays(21);

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO s2_customers 
                                (name, contact_number, product, coach_name, first_purchase_date, last_purchase_date, reorder_due_date, active, response) 
                                VALUES (@Name, @ContactNumber, @Product, @CoachName, @FirstPurchaseDate, @LastPurchaseDate, @ReorderDueDate, @Active, @Response)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", dto.Name);
                        cmd.Parameters.AddWithValue("@ContactNumber", dto.ContactNumber);
                        cmd.Parameters.AddWithValue("@Product", dto.Product);
                        cmd.Parameters.AddWithValue("@CoachName", dto.CoachName);
                        cmd.Parameters.AddWithValue("@FirstPurchaseDate", dto.FirstPurchaseDate);
                        cmd.Parameters.AddWithValue("@LastPurchaseDate", lastPurchaseDate);
                        cmd.Parameters.AddWithValue("@ReorderDueDate", reorderDueDate);
                        cmd.Parameters.AddWithValue("@Active", dto.Active);
                        cmd.Parameters.AddWithValue("@Response", dto.Response);

                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DAL S2Customer Insert Error: {ex.Message}");
                throw;
            }
        }

        public int GetActiveCustomersLast30DaysCount()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    // Matching SQL criteria per your requirements against s2_customers table
                    string query = "SELECT COUNT(*) FROM s2_customers WHERE active = 'Yes' AND last_purchase_date >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        conn.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DAL GetActiveCustomersLast30DaysCount Error: {ex.Message}");
                return 0;
            }
        }
    }
}