using System;

namespace HFNC_Coaches.Data.DTO
{
    public class PeopleRegistryDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EntryType { get; set; } = string.Empty;
        public string? InvitedBy { get; set; }
        public string CurrentStatus { get; set; } = "New";
        public string SystemStage { get; set; } = string.Empty;
        public DateTime? LastContactDate { get; set; }
        public string? Notes { get; set; }
    }
}