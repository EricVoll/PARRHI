using PARRHI.Objects.Holograms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.TriggerActions
{
    public class SetHoloStateAction : TriggerAction
    {
        readonly Container containerReference;

        public SetHoloStateAction(string id, List<Hologram> holograms, bool stateToSet ) : base(id)
        {
            this.id = id;
            Holograms = holograms;
            StateToSet = stateToSet;
        }


        readonly bool StateToSet;
        readonly List<Hologram> Holograms;


        public override void Trigger()
        {
            base.Trigger();
            foreach (var hologram in Holograms)
            {
                hologram.Active = StateToSet;
            }
        }
        protected override void Trigger(object param)
        {
            base.Trigger(param);
            foreach (var hologram in Holograms)
            {
                hologram.Active = StateToSet;
            }
        }
    }
}
