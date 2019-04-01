using FanucControllerLibrary.ControllCommanderClasses;
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
        public MoveRobotAction(string id, Point tcpTargetPoint, FanucControllerLibrary.ControllCommander commander) : base(id)
        {
            Commander = commander;
            this.tcp = tcpTargetPoint;
        }

        FanucControllerLibrary.ControllCommander Commander { get; set; }

        Point tcp { get; set; }

        public override void Trigger()
        {
            base.Trigger();
            Commander.MoveDelta((int)tcp.X, (int)tcp.Y, (int)tcp.Z, 0, 0, 0);
        }
        protected override void Trigger(object param)
        {
            base.Trigger(param);
            Commander.MoveDelta((int)tcp.X, (int)tcp.Y, (int)tcp.Z, 0, 0, 0);
        }

    }
}
