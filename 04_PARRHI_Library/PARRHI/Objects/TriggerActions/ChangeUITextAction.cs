using PARRHI.Objects.State;
using PARRHI.Objects.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.TriggerActions
{
    public class ChangeUITextAction : TriggerAction
    {
        public ChangeUITextAction(string id, string textToSet, Action<string> updateUITextCallBack) : base(id)
        {
            setUIText = updateUITextCallBack;
            this.textToSet = textToSet;
        }

        readonly string textToSet;
        readonly Action<string> setUIText;

        public override void Trigger()
        {
            base.Trigger();
            setUIText(textToSet);
        }
        protected override void Trigger(object param)
        {
            base.Trigger(param);
            setUIText(textToSet);
        }

    }
}
