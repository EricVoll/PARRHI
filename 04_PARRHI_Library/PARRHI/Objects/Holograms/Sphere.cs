using PARRHI.Objects.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Holograms
{
    public class Sphere : Hologram
    {
        public Sphere(string id, Point point, double radius, string renderMode, bool active = true) :this(id,point,radius, active)
        {
            RenderMode = renderMode;
        }

        public Sphere(string id, Point point, double radius, bool active = true)
        {
            this.id = id;
            Point1 = point;
            Radius = radius;
        }

        public double Radius { get; set; }
        public Point Point1 { get; set; }
    }
}
