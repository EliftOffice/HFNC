using System.Collections.Generic;

namespace HFNC_Coaches.Data
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public int TotalCount { get; set; }
    }
}
