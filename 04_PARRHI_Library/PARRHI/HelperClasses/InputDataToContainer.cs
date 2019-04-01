using PARRHI.Objects;
using PARRHI.Objects.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.HelperClasses
{
    public class InputDataToContainer
    {
        public InputDataToContainer(InputData inputData)
        {
            InputData = inputData;
        }
        private InputData InputData;
        private Container Container;
        public Container ConvertToContainer()
        {
            Container = new Container();

            Container.Points = ExtractPoints(InputData);

            return Container;
        }

        private List<IPoint> ExtractPoints(InputData inputData)
        {
            object[] points = inputData.Points.Items;
            List<IPoint> importedPoints = new List<IPoint>();
            foreach (var item in points)
            {
                IPoint point;
                var camPoint = item as InputDataPointsPointCamera;
                var robPoint = item as InputDataPointsPointRobot;
                var fixPoint = item as InputDataPointsPointFix;
                if (camPoint != null)
                {
                    point = new PointCamera(camPoint.name);
                }
                else if(robPoint != null)
                {
                    point = new PointRobot(robPoint.name, (double)robPoint.Scale, robPoint.J1, robPoint.J2);
                }
                else if(fixPoint != null)
                {
                    point = new PointFix(fixPoint.name, (double)fixPoint.X, (double)fixPoint.Y, (double)fixPoint.Z);
                }
                else
                {
                    throw new Exception($"The type {item.GetType()} could not be converted into a valid point");
                }

                importedPoints.Add(point);
            }

            return importedPoints;
        }

    }
}
