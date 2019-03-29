using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Points
{
    public class PointRobot : Point
    {
        public PointRobot(string id, double x, double y, double z, double scalar, int j1, int j2) : base(id, x, y, z)
        {
            this.Scalar = scalar;
            this.Joint1Index = j1;
            this.Joint2Index = j2;
        }

        public double Scalar { get; set; }
        public int Joint1Index { get; set; }
        public int Joint2Index { get; set; }

        /// <summary>
        /// Sets the Coordinates to its position between the two specified joints depending of the Scalar value
        /// </summary>
        public void UpdatePoint(Point[] RobotJoints)
        {
            var joint1 = RobotJoints[Joint1Index];
            var joint2 = RobotJoints[Joint2Index];
            var direction = joint2 - joint1;
            var point = joint1 + Scalar * direction;
            SetCoords(point);
        }
    }
}
