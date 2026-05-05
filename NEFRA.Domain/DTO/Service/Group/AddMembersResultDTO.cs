using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Service.Group
{
    public class AddMembersResultDTO
    {
        public string GroupId { get; set; } = default!;
        public List<AddedMemberDTO> Members { get; set; } = new();
    }
}
