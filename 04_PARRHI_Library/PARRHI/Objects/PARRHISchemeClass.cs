﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PARRHI.Objects
{
    //------------------------------------------------------------------------------
    // <auto-generated>
    //     Dieser Code wurde von einem Tool generiert.
    //     Laufzeitversion:4.0.30319.42000
    //
    //     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
    //     der Code erneut generiert wird.
    // </auto-generated>
    //------------------------------------------------------------------------------

    // 
    // This source code was auto-generated by xsd, Version=4.7.2053.0.
    // 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "PARRHI", IsNullable = false)]
    public partial class InuptData
    {

        /// <remarks/>
        public InuptDataVariables Variables;

        /// <remarks/>
        public InuptDataPoints Points;

        /// <remarks/>
        public InuptDataHolograms Holograms;

        /// <remarks/>
        public InuptDataEvents Events;

        /// <remarks/>
        public InuptDataUIText UIText;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataVariables
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Int")]
        public InuptDataVariablesInt[] Int;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataVariablesInt
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public byte Value;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataPoints
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PointCamera", typeof(InuptDataPointsPointCamera))]
        [System.Xml.Serialization.XmlElementAttribute("PointFix", typeof(InuptDataPointsPointFix))]
        [System.Xml.Serialization.XmlElementAttribute("PointRobot", typeof(InuptDataPointsPointRobot))]
        public object[] Items;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataPointsPointCamera
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataPointsPointFix
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte X;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte Y;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte Z;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataPointsPointRobot
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte J1;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte J2;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Scale;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataHolograms
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Sphere")]
        public InuptDataHologramsSphere[] Sphere;

        /// <remarks/>
        public InuptDataHologramsZylinder Zylinder;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataHologramsSphere
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string point;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte radius;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataHologramsZylinder
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string point1;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string point2;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte radius;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataEvents
    {

        /// <remarks/>
        public InuptDataEventsTrigger Trigger;

        /// <remarks/>
        public InuptDataEventsHandler Handler;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataEventsTrigger
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TimeTrigger")]
        public InuptDataEventsTriggerTimeTrigger[] TimeTrigger;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DistanceTrigger")]
        public InuptDataEventsTriggerDistanceTrigger[] DistanceTrigger;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("VarTrigger")]
        public InuptDataEventsTriggerVarTrigger[] VarTrigger;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataEventsTriggerTimeTrigger
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool canTrigger;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte timeSinceActivation;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string eventHadler1;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string eventHandler2;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataEventsTriggerDistanceTrigger
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool canTrigger;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string point1;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string point2;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal distance;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string eventHandler1;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string eventHandler2;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataEventsTriggerVarTrigger
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool canTrigger;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string varName;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte triggerValue;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string eventHadler1;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string eventHandler2;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataEventsHandler
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ChangeUITextEventHandler", typeof(InuptDataEventsHandlerChangeUITextEventHandler))]
        [System.Xml.Serialization.XmlElementAttribute("IncrementCounterEventHandler", typeof(InuptDataEventsHandlerIncrementCounterEventHandler))]
        [System.Xml.Serialization.XmlElementAttribute("MoveRobotEventHandler", typeof(InuptDataEventsHandlerMoveRobotEventHandler))]
        [System.Xml.Serialization.XmlElementAttribute("SetRobotHandState", typeof(InuptDataEventsHandlerSetRobotHandState))]
        [System.Xml.Serialization.XmlElementAttribute("SetTriggerStateEventHandler", typeof(InuptDataEventsHandlerSetTriggerStateEventHandler))]
        public object[] Items;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataEventsHandlerChangeUITextEventHandler
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string text;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataEventsHandlerIncrementCounterEventHandler
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string intVar;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataEventsHandlerMoveRobotEventHandler
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string pointTCP;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataEventsHandlerSetRobotHandState
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string state;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataEventsHandlerSetTriggerStateEventHandler
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string triggerName;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool canTrigger;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "PARRHI")]
    public partial class InuptDataUIText
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string text;
    }


}
