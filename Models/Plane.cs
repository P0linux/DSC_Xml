using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Plane
    {
        public string Id { get; set; } = "";

        public string Model { get; set; } = "";

        public string Origin { get; set; } = "";

        public PlaneChar[] Chars { get; set; } = Array.Empty<PlaneChar>();

        public PlaneParameters Parameters { get; set; } = new PlaneParameters();

        public float Price { get; set; }
    }
}
