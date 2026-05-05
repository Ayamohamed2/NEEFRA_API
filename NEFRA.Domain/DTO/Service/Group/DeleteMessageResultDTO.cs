using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Service.Group
{
    public class DeleteMessageResultDTO
    {
        public string MessageId { get; set; } = default!;
        public string GroupId { get; set; } = default!;
    }
}
