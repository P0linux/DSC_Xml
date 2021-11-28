using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PlaneComparer : IComparer<Plane>
    {
        public int Compare(Plane? x, Plane? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return string.Compare(x.Id, y.Id, StringComparison.Ordinal);
        }
    }
}
