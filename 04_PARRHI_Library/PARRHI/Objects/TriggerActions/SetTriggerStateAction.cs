using PARRHI.Objects.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.TriggerActions
{
    class SetTriggerStateAction : TriggerAction
    {
        readonly Container containerReference;

        public SetTriggerStateAction(string id, string triggerId, bool triggerStateToSet, Container containerReference) : base(id)
        {
            this.containerReference = containerReference;
            this.triggerStateToSet = triggerStateToSet;
            this.triggerId = triggerId;
        }

        private readonly string triggerId;
        private readonly bool triggerStateToSet;

        private Trigger triggerToSet;

        public Trigger TriggerToSet
        {
            get
            {
                if (triggerToSet == null)
                {
                    triggerToSet = containerReference.Trigger.Find(x => x.id == triggerId);
                }
                return triggerToSet;
            }
        }


        public override void Trigger()
        {
            base.Trigger();
            TriggerToSet.SetCanTrigger(triggerStateToSet);
        }
        protected override void Trigger(object param)
        {
            base.Trigger(param);
            TriggerToSet.SetCanTrigger(triggerStateToSet);
        }
    }
}