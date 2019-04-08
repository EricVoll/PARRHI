using PARRHI.Objects.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.State
{
    public class World
    {
        public World()
        {

        }

        public void Update(long Time)
        {
            World.Time = Time;
        }

        public void SetUIText(string text)
        {
            Output.Instance.Log("Settings UI Text");
            SetUITextDelegate?.Invoke(text);
        }

        public Action<string> SetUITextDelegate;

        public Point CameraPosition { get; set; }
        public static long Time { get; set; }
    }
}
