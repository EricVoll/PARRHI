using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Points
{
    public class PointCamera : Point
    {
        public PointCamera(string id) : base(id)
        {

        }

        /// <summary>
        /// Update the point with coordinates from the camera
        /// </summary>
        /// <param name="cameraPoint"></param>
        public void UpdatePoint(Point cameraPoint)
        {
            SetCoords(cameraPoint);
        }
    }
}
