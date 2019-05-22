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
    public class PProgramToContainer
    {
        private readonly PProgram PProgram;
        private readonly XMLValidationResult Result;
        public PProgramToContainer(PProgram pProgram, XMLValidationResult result)
        {
            PProgram = pProgram;
            Result = result;
        }

        public Container ConvertToContainer()
        {
            var Container = new Container();

            Container.Variables = ExtractVariables(PProgram);
            Container.Points = ExtractPoints(PProgram);
            Container.Holograms = ExtractHolograms(PProgram, Container);
            Container.TriggerActions = ExtractTriggerActions(PProgram, Container);
            Container.Trigger = ExtractTrigger(PProgram, Container);

            return Container;
        }

        /// <summary>
        /// Converts all Variables to their specific class
        /// </summary>
        /// <param name="PProgram"></param>
        /// <returns></returns>
        private List<IntVariable> ExtractVariables(PProgram PProgram)
        {
            List<IntVariable> variables = new List<IntVariable>();
            foreach (var item in PProgram.Variables.Int)
            {
                IntVariable var = new IntVariable(item.name, item.Value);
                variables.Add(var);
            }
            return variables;
        }

        /// <summary>
        /// Converts all PProgramPoints to IPoint objects of their specific implementation
        /// </summary>
        /// <param name="PProgram"></param>
        /// <returns></returns>
        private List<Point> ExtractPoints(PProgram PProgram)
        {
            object[] points = PProgram.Points.Items;
            List<Point> importedPoints = new List<Point>();
            foreach (var item in points)
            {
                Point point;
                var camPoint = item as PProgramPointsPointCamera;
                var robPoint = item as PProgramPointsPointRobot;
                var fixPoint = item as PProgramPointsPointFix;
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
        /// Extracts the Holorams from the PProgram field and initializes them with their respective points
        /// </summary>
        /// <param name="PProgram"></param>
        /// <param name="Container"></param>
        /// <returns></returns>
        private List<Hologram> ExtractHolograms(PProgram PProgram, Container Container)
        {
            List<Hologram> holograms = new List<Hologram>();
            foreach (var item in PProgram.Holograms.Items)
            {
                var sphere = item as PProgramHologramsSphere;
                var zyl = item as PProgramHologramsZylinder;
                Hologram holo;
                if (sphere != null)
                {
                    Point point = Container.Points.Find(x => x.id == sphere.point);
                    CheckForNull<Sphere>(sphere.point, point);
                    holo = new Sphere(sphere.name, point, sphere.radius, sphere.renderMode, sphere.activeSpecified ? sphere.active : true);
                }
                else if (zyl != null)
                {
                    Point point1 = Container.Points.Find(x => x.id == zyl.point1);
                    Point point2 = Container.Points.Find(x => x.id == zyl.point2);
                    CheckForNull<Zylinder>(zyl.point1, point1);
                    CheckForNull<Zylinder>(zyl.point2, point1);
                    holo = new Zylinder(zyl.name, point1, point2, zyl.radius, zyl.renderMode, zyl.activeSpecified ? zyl.active : true);
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
        /// <param name="PProgram"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        private List<Trigger> ExtractTrigger(PProgram PProgram, Container container)
        {
            List<Trigger> triggers = new List<Trigger>();

            foreach (var item in PProgram.Events.Triggers.Items.Where(x => x is PProgramEventsTriggersDistanceTrigger).Cast<PProgramEventsTriggersDistanceTrigger>())
            {
                Point p1 = container.Points.Find(x => x.id == item.point1);
                Point p2 = container.Points.Find(x => x.id == item.point2);

                List<TriggerAction> actions = FindAllActions(container, item.actions);

                CheckForNull<DistanceTrigger>(item.point1, p1);
                CheckForNull<DistanceTrigger>(item.point2, p2);
                Trigger t = new DistanceTrigger(item.name, p1, p2, (double)item.distance, item.canTrigger, actions);
                triggers.Add(t);
            }

            foreach (var item in PProgram.Events.Triggers.Items.Where(x => x is PProgramEventsTriggersTimeTrigger).Cast<PProgramEventsTriggersTimeTrigger>())
            {
                List<TriggerAction> actions = FindAllActions(container, item.actions);
                Trigger t = new TimeTrigger(item.name, item.canTrigger, item.timeSinceActivation, actions);
                triggers.Add(t);
            }

            foreach (var item in PProgram.Events.Triggers.Items.Where(x => x is PProgramEventsTriggersVarTrigger).Cast<PProgramEventsTriggersVarTrigger>())
            {
                IntVariable var = container.Variables.Find(x => x.id == item.varName);

                List<TriggerAction> actions = FindAllActions(container, item.actions);

                CheckForNull<VarTrigger>(item.varName, var);
                Trigger t = new VarTrigger(item.name, var, item.triggerValue, actions);
                triggers.Add(t);
            }

            return triggers;
        }

        /// <summary>
        /// Extracts all Actions from the PProgram 
        /// </summary>
        /// <param name="PProgram"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        private List<TriggerAction> ExtractTriggerActions(PProgram PProgram, Container container)
        {
            List<TriggerAction> actions = new List<TriggerAction>();
            foreach (var item in PProgram.Events.Actions.Items)
            {
                TriggerAction t = null;
                var incAc = item as PProgramEventsActionsIncrementCounterAction;
                var chaAc = item as PProgramEventsActionsChangeUITextAction;
                var movAc = item as PProgramEventsActionsMoveRobotAction;
                var setAc = item as PProgramEventsActionsSetRobotHandStateAction;
                var triAc = item as PProgramEventsActionsSetTriggerStateAction;
                var holAc = item as PProgramEventsActionsSetHologramStateAction;

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
                else if (holAc != null)
                {

                    List<Hologram> GetHologram(string names)
                    {
                        List<Hologram> holograms = new List<Hologram>();
                        if (names != null)
                        {
                            string[] nameList = names.Split(' ');
                            foreach (var name in nameList)
                            {
                                var holo = container.Holograms.FirstOrDefault(x => x.id == name);
                                CheckForNull<SetHoloStateAction>(holAc.name, holo);

                                if (holo != null)
                                    holograms.Add(holo);
                            }
                        }
                        return holograms;
                    }


                    List<Hologram> onHolograms = GetHologram(holAc.onHolograms);
                    List<Hologram> offHolograms = GetHologram(holAc.offHolograms);

                    t = new SetHoloStateAction(holAc.name, onHolograms, offHolograms);
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


        private List<TriggerAction> FindAllActions(Container container, string actions)
        {
            string[] triggerActionNames = actions.Split(' ');
            List<TriggerAction> actionList = new List<TriggerAction>();
            foreach (var triggerActionName in triggerActionNames)
            {
                TriggerAction action = container.TriggerActions.Find(x => x.id == triggerActionName);
                CheckForNull<DistanceTrigger>(triggerActionName, action);
                if (action != null)
                    actionList.Add(action);
            }
            return actionList;
        }

    }
}
