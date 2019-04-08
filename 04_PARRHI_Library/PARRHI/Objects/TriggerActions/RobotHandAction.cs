using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.TriggerActions
{
    public class RobotHandAction : TriggerAction
    {
        public RobotHandAction(string id, string stateToSet, Action<bool> SetHand) : base(id)
        {
            this.SetHand = SetHand;
            StateToSet = stateToSet;
        }

        Action<bool> SetHand;
        public string StateToSet { get; set; }

        public override void Trigger()
        {
            base.Trigger();
            SetHand(StateToSet == "open");
        }
        protected override void Trigger(object param)
        {
            base.Trigger(param);
            SetHand(StateToSet == "open");
        }

    }
}