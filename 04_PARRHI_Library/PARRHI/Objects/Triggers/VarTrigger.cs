using PARRHI.Objects.TriggerActions;
using PARRHI.Objects.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Triggers
{
    public class VarTrigger : Trigger
    {
        public VarTrigger(string id, IntVariable variable, int triggerValue, TriggerAction triggerAction1, TriggerAction triggerAction2) : this(id, variable, triggerValue, triggerAction1)
        {
            TriggerAction2 = triggerAction2;
        }

        public VarTrigger(string id, IntVariable variable, int triggerValue, TriggerAction triggerAction1) : this(id, variable, triggerValue)
        {
            TriggerAction1 = triggerAction1;
        }

        public VarTrigger(string id, IntVariable variable, int triggerValue) : base(id)
        {
            CheckTrigger = CheckTriggerFunc;
            Variable = variable;
            TriggerValue = triggerValue;
        }

        public IntVariable Variable { get; set; }
        public int TriggerValue { get; set; }

        private bool CheckTriggerFunc(object param)
        {
            return Variable.GetValue() == TriggerValue;
        }
    }
}
