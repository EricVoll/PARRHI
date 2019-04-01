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
        public MoveRobotAction(string id, Point tcpTargetPoint, Action<int,int,int> MoveDelta) : base(id)
        {
            this.tcp = tcpTargetPoint;
            this.MoveDelta = MoveDelta;
        }
        

        Point tcp { get; set; }
        readonly Action<int, int, int> MoveDelta;

        public override void Trigger()
        {
            base.Trigger();
            MoveDelta((int)tcp.X, (int)tcp.Y, (int)tcp.Z);
        }
        protected override void Trigger(object param)
        {
            base.Trigger(param);
            MoveDelta((int)tcp.X, (int)tcp.Y, (int)tcp.Z);
        }

    }
}
