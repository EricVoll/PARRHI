using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Triggers
{
    public class Trigger : BaseElement.Element
    {

        public Trigger(string id, bool canTrigger) : this(id)
        {
            this.CanTrigger = canTrigger;
        }
        public Trigger(string id)
        {
            this.id = id;
        }

        protected bool CanTrigger { get; set; } = true;


        /// <summary>
        /// Sets the CanTrigger flag and invokes an event
        /// </summary>
        /// <param name="value"></param>
        public void SetCanTrigger(bool value) { CanTrigger = value; CanTriggerSet?.Invoke(); Output.Instance.Error($"Changed CanTrigger to  {value}"); }
        public TriggerActions.TriggerAction TriggerAction1 { get; set; }
        public TriggerActions.TriggerAction TriggerAction2 { get; set; }

        /// <summary>
        /// Event to be overriden by children classes to notify that CanTrigger was set
        /// </summary>
        protected Action CanTriggerSet;

        /// <summary>
        /// Function to be overriden by children classes to check if trigger can trigger or not.
        /// <para>Returns true if conditions are met so that trigger can invoke</para>
        /// </summary>
        protected Func<object, bool> CheckTrigger = (o) => { return false; };

        /// <summary>
        /// Action to be overriden by children classes to trigger all TriggerActions.
        /// </summary>
        protected Action TriggerAction;

        /// <summary>
        /// Checks if the trigger can invoke or not
        /// </summary>
        public void Check(object triggerParam)
        {
            if (CheckTrigger.Invoke(triggerParam) && CanTrigger)
            {
                Output.Instance.Log($"Executing Trigger {id}");
                if (TriggerAction != null)
                    TriggerAction();
                else DefaultTriggerAction();
            }
        }

        /// <summary>
        /// Triggers both EventHandlers without parameters
        /// </summary>
        void DefaultTriggerAction()
        {
            CanTrigger = false;
            TriggerAction1?.Trigger();
            TriggerAction2?.Trigger();
        }
    }
}
