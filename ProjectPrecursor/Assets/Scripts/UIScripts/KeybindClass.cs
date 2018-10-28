using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;


public class KeybindClass {
    /// <summary>
    /// Use the class for saving data XML, use the dictionary for gameplay code
    /// </summary>

    [XmlAttribute("controlName")]
    public string controlName;
    [XmlAttribute("keyCodeValue")]
    public KeyCode keyCodeValue;
    

    //public KeybindClass(string ctl, KeyCode kc) 
    //{
    //    controlName = ctl;
    //    keyCodeValue = kc;
    //}
}
