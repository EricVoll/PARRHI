﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.7.2053.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="pars")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="pars", IsNullable=false)]
public partial class parsInputData {
    
    private parsInputDataVariables variablesField;
    
    private parsInputDataSpacialDefinitions spacialDefinitionsField;
    
    private string hologramDefinitionsField;
    
    private string uITextField;
    
    private string eventsField;
    
    private string[] textField;
    
    /// <remarks/>
    public parsInputDataVariables Variables {
        get {
            return this.variablesField;
        }
        set {
            this.variablesField = value;
        }
    }
    
    /// <remarks/>
    public parsInputDataSpacialDefinitions SpacialDefinitions {
        get {
            return this.spacialDefinitionsField;
        }
        set {
            this.spacialDefinitionsField = value;
        }
    }
    
    /// <remarks/>
    public string HologramDefinitions {
        get {
            return this.hologramDefinitionsField;
        }
        set {
            this.hologramDefinitionsField = value;
        }
    }
    
    /// <remarks/>
    public string UIText {
        get {
            return this.uITextField;
        }
        set {
            this.uITextField = value;
        }
    }
    
    /// <remarks/>
    public string Events {
        get {
            return this.eventsField;
        }
        set {
            this.eventsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text {
        get {
            return this.textField;
        }
        set {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="pars")]
public partial class parsInputDataVariables {
    
    private object[] itemsField;
    
    private string[] textField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Int", typeof(parsInputDataVariablesInt))]
    [System.Xml.Serialization.XmlElementAttribute("String", typeof(parsInputDataVariablesString))]
    public object[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text {
        get {
            return this.textField;
        }
        set {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="pars")]
public partial class parsInputDataVariablesInt {
    
    private string nameField;
    
    private byte valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="pars")]
public partial class parsInputDataVariablesString {
    
    private string nameField;
    
    private string valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="pars")]
public partial class parsInputDataSpacialDefinitions {
    
    private object[] itemsField;
    
    private string[] textField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("PointDefinitionFix", typeof(parsInputDataSpacialDefinitionsPointDefinitionFix))]
    [System.Xml.Serialization.XmlElementAttribute("PointDefinitionRobot", typeof(parsInputDataSpacialDefinitionsPointDefinitionRobot))]
    public object[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text {
        get {
            return this.textField;
        }
        set {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="pars")]
public partial class parsInputDataSpacialDefinitionsPointDefinitionFix {
    
    private string nameField;
    
    private byte xField;
    
    private byte yField;
    
    private byte zField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte X {
        get {
            return this.xField;
        }
        set {
            this.xField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte Y {
        get {
            return this.yField;
        }
        set {
            this.yField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte Z {
        get {
            return this.zField;
        }
        set {
            this.zField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2053.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="pars")]
public partial class parsInputDataSpacialDefinitionsPointDefinitionRobot {
    
    private string nameField;
    
    private byte j1Field;
    
    private byte j2Field;
    
    private decimal scaleField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte J1 {
        get {
            return this.j1Field;
        }
        set {
            this.j1Field = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte J2 {
        get {
            return this.j2Field;
        }
        set {
            this.j2Field = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Scale {
        get {
            return this.scaleField;
        }
        set {
            this.scaleField = value;
        }
    }
}
