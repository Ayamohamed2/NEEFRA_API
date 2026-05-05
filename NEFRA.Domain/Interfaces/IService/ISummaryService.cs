using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.DTO.Service.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface ISummaryService
    {
        Task<SummaryDTo<List<object>>> Summary(string visitId, string baseurl);
        Task<SummaryDTo<List<object>>> SummaryForUser(string visitId, string baseurl, string UserId);
    }
}
