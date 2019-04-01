using PARRHI.Objects.Triggers;
using PARRHI.Objects.Holograms;
using PARRHI.Objects.Points;
using PARRHI.Objects.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PARRHI.Objects.TriggerActions;
using FanucControllerLibrary.DataTypes;

namespace PARRHI.Objects
{
    public class Container
    {
        public Container()
        {
            State = new State.State();
        }

        public void Update(Vector6 q, Point cameraPoint, long timeSinceStartup)
        {
            State.UpdateState(q, cameraPoint, timeSinceStartup);

            Point[] joints = State.GetJointPositions();
            UpdatePoints(joints, cameraPoint);
        }

        public Objects.State.State State { get; set; }
        public List<Point> Points { get; internal set; }
        public List<IntVariable> Variables { get; internal set; }
        public List<Hologram> Holograms { get; internal set; }
        public List<Trigger> Trigger { get; internal set; }
        public List<TriggerAction> TriggerActions {get; internal set;}

        /// <summary>
        /// Update all points with data form the robot and from extern
        /// </summary>
        /// <param name="jointPositions"></param>
        /// <param name="cameraPoint"></param>
        public void UpdatePoints(Point[] jointPositions, Point cameraPoint)
        {
            foreach (var point in Points.Where(x => x is PointRobot).Cast<PointRobot>())
            {
                point.UpdatePoint(jointPositions);
            }
            foreach (var point in Points.Where(x => x is PointCamera).Cast<PointCamera>())
            {
                point.UpdatePoint(cameraPoint);
            }
        }
    }
}
