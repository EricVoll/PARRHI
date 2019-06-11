using PARRHI.Objects.State;
using PARRHI.Objects.TriggerActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Triggers
{
    public class TimeTrigger : Trigger
    {
        public TimeTrigger(string id, bool canTrigger, long triggertime, List<TriggerAction> triggerActions) : this(id, canTrigger, triggertime)
        {
            TriggerActions = triggerActions;
        }
        public TimeTrigger(string id, bool canTrigger, long triggertime) : base(id, canTrigger)
        {
            CanTriggerSet = SetResetTime;
            TriggerTime = triggertime;
            CheckTrigger = CheckTriggerFunc;
        }

        /// <summary>
        /// Triggertime in ms after activation
        /// </summary>
        private long TriggerTime { get; set; } = 0;

        private long TriggerTimeSinceReset { get; set; } = 0;


        public void SetResetTime()
        {
            TriggerTimeSinceReset = World.Time;
        }

        private bool CheckTriggerFunc(object currentTime)
        {
            return ((long)currentTime - TriggerTimeSinceReset) >= TriggerTime;
        }
    }
}
