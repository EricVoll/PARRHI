using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.TriggerActions
{
    public class RobotHandAction : TriggerAction
    {
        public RobotHandAction(string id, string stateToSet, Container container) : base(id)
        {
            this.Container = container;
            StateToSet = stateToSet;
        }

        Container Container { get; set; }
        public string StateToSet { get; set; }

        public override void Trigger()
        {
            base.Trigger();
            Container.State.Robot.SetHand(StateToSet == "open");
        }
        protected override void Trigger(object param)
        {
            base.Trigger(param);
            Container.State.Robot.SetHand(StateToSet == "open");
        }

    }
}