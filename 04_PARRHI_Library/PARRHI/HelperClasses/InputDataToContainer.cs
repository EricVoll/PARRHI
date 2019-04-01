using PARRHI.Objects;
using PARRHI.Objects.Triggers;
using PARRHI.Objects.TriggerActions;
using PARRHI.Objects.Holograms;
using PARRHI.Objects.Points;
using PARRHI.Objects.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.HelperClasses
{
    public class InputDataToContainer
    {
        private readonly InputData InputData;
        public InputDataToContainer(InputData inputData)
        {
            InputData = inputData;
        }

        public Container ConvertToContainer()
        {
            var Container = new Container();

            Container.Variables = ExtractVariables(InputData);
            Container.Points = ExtractPoints(InputData);
            Container.Holograms = ExtractHolograms(InputData, Container);
            Container.Trigger = ExtreactTrigger(InputData, Container);

            return Container;
        }

        /// <summary>
        /// Converts all Variables to their specific class
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private List<IntVariable> ExtractVariables(InputData inputData)
        {
            List<IntVariable> variables = new List<IntVariable>();
            foreach (var item in inputData.Variables.Int)
            {
                IntVariable var = new IntVariable(item.name, item.Value);
                variables.Add(var);
            }
            return variables;
        }

        /// <summary>
        /// Converts all InputDataPoints to IPoint objects of their specific implementation
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private List<Point> ExtractPoints(InputData inputData)
        {
            object[] points = inputData.Points.Items;
            List<Point> importedPoints = new List<Point>();
            foreach (var item in points)
            {
                Point point;
                var camPoint = item as InputDataPointsPointCamera;
                var robPoint = item as InputDataPointsPointRobot;
                var fixPoint = item as InputDataPointsPointFix;
                if (camPoint != null)
                {
                    point = new PointCamera(camPoint.name);
                }
                else if (robPoint != null)
                {
                    point = new PointRobot(robPoint.name, (double)robPoint.Scale, robPoint.J1, robPoint.J2);
                }
                else if (fixPoint != null)
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

        /// <summary>
        /// Extracts the Holorams from the InputData field and initializes them with their respective points
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="Container"></param>
        /// <returns></returns>
        private List<Hologram> ExtractHolograms(InputData inputData, Container Container)
        {
            List<Hologram> holograms = new List<Hologram>();
            foreach (var item in InputData.Holograms.Items)
            {
                var sphere = item as InputDataHologramsSphere;
                var zyl = item as InputDataHologramsZylinder;
                Hologram holo;
                if(sphere != null)
                {
                    Point point = Container.Points.Find(x => x.id == sphere.point);
                    holo = new Sphere(sphere.name, point, sphere.radius);
                }
                else if(zyl != null)
                {
                    Point point1 = Container.Points.Find(x => x.id == zyl.point1);
                    Point point2 = Container.Points.Find(x => x.id == zyl.point2);
                    holo = new Zylinder(zyl.name, point1, point2, zyl.radius);
                }
                else
                {
                    throw new Exception($"The type {item.GetType()} could not be converted into a valid point");
                }
                holograms.Add(holo);
            }
            return holograms;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        private List<Trigger> ExtreactTrigger(InputData inputData, Container container)
        {
            return null;
        }

    }
}
