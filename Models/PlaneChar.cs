using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PlaneChar
    {
        public PlaneType Type { get; set; }

        public int PlaceNumber { get; set; }

        public bool HasAmmunition { get; set; }

        public int RocketNumber { get; set; }

        public bool HasRadar { get; set; }
    }
}
