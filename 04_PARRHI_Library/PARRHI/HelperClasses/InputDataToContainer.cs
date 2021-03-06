﻿using PARRHI.Objects;
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
                    bool active = true;
                    if(sphere.visibility == "hidden")
                        active = false;
                    holo = new Sphere(sphere.name, point, sphere.radius, sphere.renderMode,  active);
                }
                else if (zyl != null)
                {
                    Point point1 = Container.Points.Find(x => x.id == zyl.point1);
                    Point point2 = Container.Points.Find(x => x.id == zyl.point2);
                    CheckForNull<Zylinder>(zyl.point1, point1);
                    CheckForNull<Zylinder>(zyl.point2, point1);
                    bool active = true;
                    if (zyl.visibility == "hidden")
                        active = false;
                    holo = new Zylinder(zyl.name, point1, point2, zyl.radius, zyl.renderMode, active);
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

            if (PProgram.Events.Triggers.Items == null) return triggers;

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

            if (PProgram.Events.Actions.Items == null) return actions;

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
                    chaAc.text = System.Text.RegularExpressions.Regex.Unescape(chaAc.text);
                    t = new ChangeUITextAction(chaAc.name, chaAc.text, container.State.World.SetUIText);
                }
                else if (movAc != null)
                {
                    t = ConstructAction(container, movAc);
                }
                else if (setAc != null)
                {
                    t = new RobotHandAction(setAc.name, setAc.state, container);
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

                if (t != null)
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

        /// <summary>
        /// Parses a string with ids of actions and returns the list of action refernces
        /// </summary>
        /// <param name="container"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private MoveRobotAction ConstructAction(Container container, PProgramEventsActionsMoveRobotAction item)
        {
            string target = item.target;

            string[] targetData = target.Split(' ');

            if (targetData.Length == 6 || targetData.Length == 3)
            {
                FanucControllerLibrary.DataTypes.Vector6 vector = new FanucControllerLibrary.DataTypes.Vector6();
                //Vector6 Mode
                for (int i = 0; i < targetData.Length; i++)
                {
                    if (Double.TryParse(targetData[i], out double nr))
                    {
                        vector[i] = nr;
                    }
                    else
                    {
                        Result.AddConversionError(new XMLValidationError($"{item.name}: Target Data ({targetData[i]}) cannot be parsed into a valid double number.", System.Xml.Schema.XmlSeverityType.Error, null));
                        return null;
                    }
                }
                if (targetData.Length == 3)
                {
                    vector[3] = 0;
                    vector[4] = 0;
                    vector[5] = 0;
                }
                if (item.mode != "t" && item.mode != "j")
                {
                    Result.AddConversionError(new XMLValidationError($"{item.name}: Mode ({item.mode}) is not a valid character. Either 't' or 'j' for task space and joint space respectivley.", System.Xml.Schema.XmlSeverityType.Error, null));
                    return null;
                }

                return new MoveRobotAction(item.name, container, vector, item.mode);
            }

            if (targetData.Length == 1)
            {
                Point p = container.Points.Find(x => x.id == item.target);
                CheckForNull<MoveRobotAction>(item.name, p);
                if (p == null) return null;
                else return new MoveRobotAction(item.name, container, p);
            }


            Result.AddConversionError(new XMLValidationError($"{item.name}: Target Data ({target}) cannot be parsed into a valid object.", System.Xml.Schema.XmlSeverityType.Error, null));
            return null;
        }
    }
}
