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

        public DashboardStatsDTO GetDashboardStats()
        {
            var stats = _dal.GetDashboardStats();

            // Activity Scale Logic based on Today's count
            if (stats.TodayCount <= 0)
            {
                stats.SystemHealthText = "🚫 System Stalled";
                stats.SystemHealthColor = "#e74c3c"; // Red
            }
            else if (stats.TodayCount >= 1 && stats.TodayCount <= 3)
            {
                stats.SystemHealthText = "⚠️ Low Activity";
                stats.SystemHealthColor = "#f39c12"; // Orange
            }
            else if (stats.TodayCount >= 4 && stats.TodayCount <= 7)
            {
                stats.SystemHealthText = "✅ Healthy";
                stats.SystemHealthColor = "#27ae60"; // Green
            }
            else
            {
                stats.SystemHealthText = "🔥 Strong Momentum";
                stats.SystemHealthColor = "#2d6a54"; // Dark Green
            }

            return stats;
        }
    }
}