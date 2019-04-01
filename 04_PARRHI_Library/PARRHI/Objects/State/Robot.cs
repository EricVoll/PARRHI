using FanucControllerLibrary.DataTypes;
using PARRHI.HelperClasses;
using PARRHI.Objects.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects
{
    public class Robot
    {
        public static Robot instance { get; set; } = new Robot();

        //The location of all joints
        public Point[] Joints { get; set; }
        Vector6 CurrentTarget { get; set; }
        Vector6 CurrentTCP { get; set; }

        readonly ForwardKinematics forwardKinematics = new ForwardKinematics();

        public Robot()
        {
            Joints = new Point[6];
        }

        /// <summary>
        /// This method calculates all joints positions.
        /// </summary>
        /// <param name="jointAngles"></param>
        /// <returns></returns>
        public void UpdateRobotPositions(Vector6 jointAngles)
        {
            SetJoints(forwardKinematics.CaluclateForwardKinematics(jointAngles));
        }

        public void SetJoints(Point[] jointLocations)
        {
            if(jointLocations.Length == Joints.Length)
            {
                Joints = jointLocations;
            }
            else
            {
                Output.Instance.Error($"Length of JointArray does not match existing Joint array");
            }
        }




        public void MoveDelta(int x, int y, int z)
        {

        }

        internal void SetHand(bool state)
        {

        }
    }
}
