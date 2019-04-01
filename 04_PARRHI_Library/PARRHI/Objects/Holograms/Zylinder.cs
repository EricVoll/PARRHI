﻿using PARRHI.Objects.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Holograms
{
    public class Zylinder : Hologram
    {
        public Zylinder(string id, Point point1, Point point2, double radius)
        {
            this.id = id;
            Point1 = point1;
            Point2 = point2;
            Radius = radius;
        }

        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public Double Radius { get; set; }

    }
}
