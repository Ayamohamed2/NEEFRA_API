using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NEEFRA.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Entities.AI
{
    public class Ai_A: BaseEntity
    {

        public string Name { get; set; }

        public string Language { get; set; }

        public string? Type { get; set; }

        public string?  text{ get; set; }
        public string AudioUrl { get; set; }
    }
}
