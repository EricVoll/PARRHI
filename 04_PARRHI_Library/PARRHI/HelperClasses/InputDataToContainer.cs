using PARRHI.Objects;
using PARRHI.Objects.Triggers;
using PARRHI.Objects.TriggerActions;
using PARRHI.Objects.Holograms;
using PARRHI.Objects.Points;
using PARRHI.Objects.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PARRHI.Objects.State;
using PARRHI.HelperClasses.XML;

namespace PARRHI.HelperClasses
{
    public class InputDataToContainer
    {
        private readonly InputData InputData;
        private readonly XMLValidationResult Result;
        public InputDataToContainer(InputData inputData, XMLValidationResult result)
        {
            InputData = inputData;
            Result = result;
        }

        public Container ConvertToContainer()
        {
            var Container = new Container();

            Container.Variables = ExtractVariables(InputData);
            Container.Points = ExtractPoints(InputData);
            Container.Holograms = ExtractHolograms(InputData, Container);
            Container.TriggerActions = ExtractTriggerActions(InputData, Container);
            Container.Trigger = ExtractTrigger(InputData, Container);

            return Container;
        }

        /// <summary>
        /// Converts all Variables to their specific class
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private List<IntVariable> ExtractVariables(InputData inputData)
        {
            List<IntVariable> variables = new List<IntVariable>();
            foreach (var item in inputData.Variables.Int)
            {
                IntVariable var = new IntVariable(item.name, item.Value);
                variables.Add(var);
            }
            return variables;
        }

        /// <summary>
        /// Converts all InputDataPoints to IPoint objects of their specific implementation
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private List<Point> ExtractPoints(InputData inputData)
        {
            object[] points = inputData.Points.Items;
            List<Point> importedPoints = new List<Point>();
            foreach (var item in points)
            {
                Point point;
                var camPoint = item as InputDataPointsPointCamera;
                var robPoint = item as InputDataPointsPointRobot;
                var fixPoint = item as InputDataPointsPointFix;
                if (camPoint != null)
                {
                    point = new PointCamera(camPoint.name);
                }
                else if (robPoint != null)
                {
                    point = new PointRobot(robPoint.name, (double)robPoint.Scale, robPoint.J1, robPoint.J2);
                }
                else if (fixPoint != null)
                {
                    point = new PointFix(fixPoint.name, (double)fixPoint.X, (double)fixPoint.Y, (double)fixPoint.Z);
                }
                else
                {
                    throw new Exception($"The type {item.GetType()} could not be converted into a valid point");
                }

                importedPoints.Add(point);
            }

            return importedPoints;
        }

        /// <summary>
        /// Extracts the Holorams from the InputData field and initializes them with their respective points
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="Container"></param>
        /// <returns></returns>
        private List<Hologram> ExtractHolograms(InputData inputData, Container Container)
        {
            List<Hologram> holograms = new List<Hologram>();
            foreach (var item in inputData.Holograms.Items)
            {
                var sphere = item as InputDataHologramsSphere;
                var zyl = item as InputDataHologramsZylinder;
                Hologram holo;
                if (sphere != null)
                {
                    Point point = Container.Points.Find(x => x.id == sphere.point);
                    CheckForNull<Sphere>(sphere.point, point);
                    holo = new Sphere(sphere.name, point, sphere.radius);
                }
                else if (zyl != null)
                {
                    Point point1 = Container.Points.Find(x => x.id == zyl.point1);
                    Point point2 = Container.Points.Find(x => x.id == zyl.point2);
                    CheckForNull<Zylinder>(zyl.point1, point1);
                    CheckForNull<Zylinder>(zyl.point2, point1);
                    holo = new Zylinder(zyl.name, point1, point2, zyl.radius);
                }
                else
                {
                    throw new Exception($"The type {item.GetType()} could not be converted into a valid point");
                }
                holograms.Add(holo);
            }
            return holograms;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        private List<Trigger> ExtractTrigger(InputData inputData, Container container)
        {
            List<Trigger> triggers = new List<Trigger>();

            foreach (var item in inputData.Events.Trigger.Items.Where(x => x is InputDataEventsTriggerDistanceTrigger).Cast<InputDataEventsTriggerDistanceTrigger>())
            {
                Point p1 = container.Points.Find(x => x.id == item.point1);
                Point p2 = container.Points.Find(x => x.id == item.point2);
                TriggerAction t1 = container.TriggerActions.Find(x => x.id == item.action1);
                TriggerAction t2 = container.TriggerActions.Find(x => x.id == item.action2);
                CheckForNull<DistanceTrigger>(item.point1, p1);
                CheckForNull<DistanceTrigger>(item.point2, p2);
                CheckForNull<DistanceTrigger>(item.action1, t1);
                CheckForNull<DistanceTrigger>(item.action2, t2);
                Trigger t = new DistanceTrigger(item.name, p1, p2, (double)item.distance, item.canTrigger, t1, t2);
                triggers.Add(t);
            }

            foreach (var item in inputData.Events.Trigger.Items.Where(x => x is InputDataEventsTriggerTimeTrigger).Cast<InputDataEventsTriggerTimeTrigger>())
            {
                TriggerAction t1 = container.TriggerActions.Find(x => x.id == item.action1);
                TriggerAction t2 = container.TriggerActions.Find(x => x.id == item.action2);
                CheckForNull<TimeTrigger>(item.action1, t1);
                CheckForNull<TimeTrigger>(item.action2, t2);
                Trigger t = new TimeTrigger(item.name, item.canTrigger, item.timeSinceActivation, t1, t2);
                triggers.Add(t);
            }

            foreach (var item in inputData.Events.Trigger.Items.Where(x => x is InputDataEventsTriggerVarTrigger).Cast<InputDataEventsTriggerVarTrigger>())
            {
                IntVariable var = container.Variables.Find(x => x.id == item.varName);
                TriggerAction t1 = container.TriggerActions.Find(x => x.id == item.action1);
                TriggerAction t2 = container.TriggerActions.Find(x => x.id == item.action2);
                CheckForNull<VarTrigger>(item.action1, t1);
                CheckForNull<VarTrigger>(item.action2, t2);
                CheckForNull<VarTrigger>(item.varName, var);
                Trigger t = new VarTrigger(item.name, var, item.triggerValue, t1, t2);
                triggers.Add(t);
            }

            return triggers;
        }

        /// <summary>
        /// Extracts all Actions from the InputData 
        /// </summary>
        /// <param name="inputdata"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        private List<TriggerAction> ExtractTriggerActions(InputData inputdata, Container container)
        {
            List<TriggerAction> actions = new List<TriggerAction>();
            foreach (var item in inputdata.Events.Actions.Items)
            {
                TriggerAction t = null;
                var incAc = item as InputDataEventsActionsIncrementCounterAction;
                var chaAc = item as InputDataEventsActionsChangeUITextAction;
                var movAc = item as InputDataEventsActionsMoveRobotAction;
                var setAc = item as InputDataEventsActionsSetRobotHandStateAction;
                var triAc = item as InputDataEventsActionsSetTriggerStateAction;

                if (incAc != null)
                {
                    IntVariable var = container.Variables.Find(x => x.id == incAc.intVar);
                    CheckForNull<IncrementCounterAction>(incAc.intVar, var);
                    t = new IncrementCounterAction(incAc.name, var);
                }
                else if (chaAc != null)
                {
                    t = new ChangeUITextAction(chaAc.name, chaAc.text, container.State.World.SetUIText);
                }
                else if (movAc != null)
                {
                    Point point = container.Points.Find(x => x.id == movAc.pointTCP);
                    CheckForNull<MoveRobotAction>(movAc.pointTCP, point);
                    t = new MoveRobotAction(movAc.name, point, container.State.Robot.MoveDelta);
                }
                else if (setAc != null)
                {
                    t = new RobotHandAction(setAc.name, setAc.state, container.State.Robot.SetHand);
                }
                else if (triAc != null)
                {
                    t = new SetTriggerStateAction(triAc.name, triAc.triggerName, triAc.canTrigger, container);
                }
                else
                {
                    throw new Exception($"The type {item.GetType()} could not be converted into a valid action");
                }

                actions.Add(t);
            }
            return actions;
        }

        /// <summary>
        /// Checks the specified object for null values and creates an error if needed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="objForNullCheck"></param>
        private void CheckForNull<T>(string id, object objForNullCheck)
        {
            if (String.IsNullOrEmpty(id))
                return;
            if (objForNullCheck == null)
                Result.AddConversionError(new XMLValidationError($"Id: \"{id}\" not found in list. Type:{typeof(T)}", System.Xml.Schema.XmlSeverityType.Error, null));
        }

    }
}
