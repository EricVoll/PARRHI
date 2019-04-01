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
        public TimeTrigger(string id, bool canTrigger, long triggertime, TriggerAction triggerAction1, TriggerAction triggerAction2) : this(id, canTrigger, triggertime, triggerAction1)
        {
            TriggerAction2 = triggerAction2;
        }
        public TimeTrigger(string id, bool canTrigger, long triggertime, TriggerAction triggerAction1) : this(id, canTrigger, triggertime)
        {
            TriggerAction1 = triggerAction1;
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
        public long TriggerTime { get; set; } = 0;

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
