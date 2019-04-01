using PARRHI.Objects.Points;
using PARRHI.Objects.TriggerActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Triggers
{
    public class DistanceTrigger : Trigger
    {
        const double Tolerance = 10;
        public DistanceTrigger(string id, Point point1, Point point2, double distance, TriggerAction triggerAction1, TriggerAction triggerAction2) : this(id, point1, point2, distance, triggerAction1)
        {
            TriggerAction2 = triggerAction2;
        }
        public DistanceTrigger(string id, Point point1, Point point2, double distance, TriggerAction triggerAction1) : this(id, point1, point2, distance)
        {
            TriggerAction1 = triggerAction1;
        }
        public DistanceTrigger(string id, Point point1, Point point2, double distance) : base(id)
        {
            CheckTrigger = CheckTriggerFunc;
            P1 = point1;
            P2 = point2;
            Distance = distance;
        }

        Point P1 { get; set; }
        Point P2 { get; set; }
        double Distance { get; set; }

        private bool CheckTriggerFunc(object param)
        {
            return (P2 - P1).Magnitude < Tolerance;
        }
    }
}
