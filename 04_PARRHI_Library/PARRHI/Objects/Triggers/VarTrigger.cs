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

        public VarTrigger(string id, IntVariable variable, int triggerValue, List<TriggerAction> triggerActions) : this(id, variable, triggerValue)
        {
            TriggerActions = triggerActions;
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
