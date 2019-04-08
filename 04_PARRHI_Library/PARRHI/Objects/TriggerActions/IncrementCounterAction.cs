using PARRHI.Objects.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.TriggerActions
{
    public class IncrementCounterAction : TriggerAction
    {
        public IncrementCounterAction(string id, IntVariable variable) : base(id)
        {
            this.Variable = variable;
        }

        IntVariable Variable { get; set; }
        int CompareValue { get; set; }

        public override void Trigger()
        {
            base.Trigger();
            Output.Instance.Write($"Executing TriggerAction {id}");
            Variable.IncrementValue();
        }
        protected override void Trigger(object param)
        {
            base.Trigger(param);
            Output.Instance.Write($"Executing TriggerAction {id}");
            Variable.IncrementValue();
        }

    }
}
