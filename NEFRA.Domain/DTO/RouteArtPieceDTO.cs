using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO
{
    
        public class RouteArtPieceDTO
        {
            public string Name { get; set; } = null!;
            public int Floor { get; set; }
            public bool Valid { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public int Score { get; set; }
        }
 }
