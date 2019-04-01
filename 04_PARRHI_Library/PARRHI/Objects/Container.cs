using PARRHI.Objects.Triggers;
using PARRHI.Objects.Holograms;
using PARRHI.Objects.Points;
using PARRHI.Objects.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PARRHI.Objects.TriggerActions;

namespace PARRHI.Objects
{
    public class Container
    {
        public Container()
        {
            State = new State.State();
        }
        public Objects.State.State State { get; set; }
        public List<Point> Points { get; internal set; }
        public List<IntVariable> Variables { get; internal set; }
        public List<Hologram> Holograms { get; internal set; }
        public List<Trigger> Trigger { get; internal set; }
        public List<TriggerAction> TriggerActions {get; internal set;}
    }
}
