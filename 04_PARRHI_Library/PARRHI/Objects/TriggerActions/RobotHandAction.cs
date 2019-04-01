using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.TriggerActions
{
    public class RobotHandAction : TriggerAction
    {
        public RobotHandAction(string id, string stateToSet, FanucControllerLibrary.ControllCommander commander) : base(id)
        {
            Commander = commander;
            StateToSet = stateToSet;
        }

        FanucControllerLibrary.ControllCommander Commander { get; set; }
        public string StateToSet { get; set; }

        public override void Trigger()
        {
            base.Trigger();
            Commander.SetHand(StateToSet == "open");
        }
        protected override void Trigger(object param)
        {
            base.Trigger(param);
            Commander.SetHand(StateToSet == "open");
        }

    }
}