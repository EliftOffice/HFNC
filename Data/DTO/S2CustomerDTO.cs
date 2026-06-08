using System;

namespace HFNC_Coaches.Data.DTO
{
    public class S2CustomerDTO
    {
        public string Name { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string CoachName { get; set; } = string.Empty;
        public DateTime FirstPurchaseDate { get; set; }
        public string Active { get; set; } = "Yes";
        public string Response { get; set; } = "Good Response";
    }
}