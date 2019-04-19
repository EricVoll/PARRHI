using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Holograms
{
    public class Hologram : BaseElement.Element
    {
        public string RenderMode { get; set; } = "normal";
        public bool Active { get; set; } = true;
    }
}
