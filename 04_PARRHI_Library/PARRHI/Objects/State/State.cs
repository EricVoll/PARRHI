using FanucControllerLibrary.DataTypes;
using PARRHI.Objects.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.State
{
    public class State
    {
        public State()
        {
            Robot = new Robot();
            World = new World();
        }

        public Robot Robot { get; set; }
        public World World { get; set; }

        public void UpdateState(Vector6 jointAngles, Point CameraPosition, long timeSinceStartup)
        {
            Robot.UpdateRobotPositions(jointAngles);
            World.Update(timeSinceStartup);
        }

        public Point[] GetJointPositions()
        {
            return Robot.Joints;
        }
    }
}
