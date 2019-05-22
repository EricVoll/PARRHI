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

        public SetHoloStateAction(string id, List<Hologram> onHolograms, List<Hologram> offHolograms) : base(id)
        {
            this.onHolograms = onHolograms;
            this.offHolograms = offHolograms;
        }

        
        readonly List<Hologram> onHolograms;
        readonly List<Hologram> offHolograms;


        public override void Trigger()
        {
            base.Trigger();
            Action();
        }
        protected override void Trigger(object param)
        {
            base.Trigger(param);
            Action();
        }

        private void Action()
        {
            foreach (var hologram in onHolograms)
            {
                hologram.Active = true;
            }
            foreach (var hologram in offHolograms)
            {
                hologram.Active = false;
            }
        }
    }
}
