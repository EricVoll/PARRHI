using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Points
{
    public interface IPoint
    {
        double X { get; set; }
        double Y { get; set; }
        double Z { get; set; }
        double Magnitude { get; }
    }
}
