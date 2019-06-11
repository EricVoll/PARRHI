using FanucControllerLibrary.ControllCommanderClasses;
using FanucControllerLibrary.DataTypes;
using PARRHI.Objects.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.TriggerActions
{
    public class MoveRobotAction : TriggerAction
    {
        /// <summary>
        /// Target the given point in task space
        /// </summary>
        /// <param name="id"></param>
        /// <param name="MoveDelta"></param>
        /// <param name="targetCoordinates"></param>
        public MoveRobotAction(string id, Container container, Point targetCoordinates) : this(id, container)
        {
            TargetTCPPoint = targetCoordinates;
            Mode = "t";
        }
        /// <summary>
        /// target the given coordinates either in joint or in task space
        /// </summary>
        /// <param name="id"></param>
        /// <param name="MoveDelta"></param>
        /// <param name="targetCoordinates"></param>
        /// <param name="mode">0 = taskSpace, 1 = joint space</param>
        public MoveRobotAction(string id, Container container, Vector6 targetCoordinates, string mode) : this(id, container)
        {
            TargetTCPCoordinates = targetCoordinates;
            Mode = mode;
        }
        public MoveRobotAction(string id, Container container) : base(id)
        {
            Container = container;
        }

        Vector6 TargetTCPCoordinates;
        Point TargetTCPPoint;
        string Mode;


        private Container Container;

        public override void Trigger()
        {
            base.Trigger();
            TriggerAction();
        }
        protected override void Trigger(object param)
        {
            base.Trigger(param);
            TriggerAction();
        }

        private void TriggerAction()
        {
            if (TargetTCPPoint != null)
            {
                TargetTCPCoordinates = new Vector6(TargetTCPPoint[0], TargetTCPPoint[1], TargetTCPPoint[2], 0, 0, 0);
            }
            Container.State.Robot.MoveDelta(TargetTCPCoordinates, Mode);
        }


    }
}
