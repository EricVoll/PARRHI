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
        public DistanceTrigger(string id, Point point1, Point point2, double distance, bool canTrigger, List<TriggerAction> triggerActions) : this(id, point1, point2, distance, canTrigger)
        {
            TriggerActions = triggerActions;
        }
        public DistanceTrigger(string id, Point point1, Point point2, double distance, bool canTrigger) : base(id, canTrigger)
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
            return (P2 - P1).Magnitude < Distance;
        }
    }
}
