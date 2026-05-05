using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Service.Summary
{
    
    public class SummaryDTo<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public string MuseumName { get; set; }
        public string  VisitType { get; set; }

        public  DateTime? VisitStartTime { get; set; }

        public DateTime? VisitEndTime { get; set; }
        public bool IsInsideMuseum { get; set; }
        public string? ErrorType { get; set; }
    }
}
