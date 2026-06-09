using System;

namespace HFNC_Coaches.Data.DTO
{
    public class DashboardStatsDTO
    {
        public int TodayCount { get; set; }
        public int ActiveCount { get; set; }
        public int CustomerCount { get; set; }
        public int InactiveCount { get; set; }
        public int ProspectCount { get; set; }
        public int DistributorCount { get; set; }
        public string SystemHealthText { get; set; } = string.Empty;
        public string SystemHealthColor { get; set; } = string.Empty;
    }
}