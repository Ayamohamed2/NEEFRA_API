using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Account
{
    public sealed class ExternalUserInfo
    {
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
        public string ProviderId { get; set; } = "";
        public string? Picture { get; set; }
    }
}
