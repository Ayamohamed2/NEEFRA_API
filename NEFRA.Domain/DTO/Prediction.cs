using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO
{
    public class LabelData
    {
        public string? Label { get; set; }
        public List<LabelConfidence>? Confidences { get; set; }
    }

    public class LabelConfidence
    {
        public string? Label { get; set; }
        public double Confidence { get; set; }
    }
}
