using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.TriggerActions
{
    public class TriggerAction : BaseElement.Element
    {
        public TriggerAction(string id)
        {
            this.id = id;
        }

        protected virtual void Trigger(object param)
        {

        }

        public virtual void Trigger()
        {

        }
    }
}
