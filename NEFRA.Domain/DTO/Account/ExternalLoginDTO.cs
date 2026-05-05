using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Account
{
    public class ExternalLoginDTO
    {
        public string Provider { get; set; }   // "Google" or "Facebook"
        public string IdToken { get; set; }    // Token from client SDK
    }
}
