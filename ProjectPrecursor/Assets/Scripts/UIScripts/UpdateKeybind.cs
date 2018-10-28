using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(KeybindController))]

[ExecuteInEditMode]
public class UpdateKeybind : MonoBehaviour {

    public bool updateButton = false;
    public bool saveButton = false;
    private int controlUICount;
    private int controlUICount2;
    private KeybindController keybindScript;

    private KeybindClass[] savingKeybind;

    void Update() {
        if (updateButton)
        {
            
            UpdateTheKeybind();
            updateButton = false;
        }
        else if (saveButton)
        {
            SaveTheKeybind();
            saveButton = false;
        }
    }

    public void LoadTheKeybind() //Update XML(currKeybind) To UI
    {
        keybindScript = GetComponent<KeybindController>();
        controlUICount = keybindScript.col1.transform.childCount;
        controlUICount2 = keybindScript.col2.transform.childCount;

        for (int i = 0; i < controlUICount; i++)
        {
            keybindScript.col1.transform.GetChild(i).GetComponent<Text>().text = GlobalSettings.currKeybind[i].controlName;
            keybindScript.col1.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = GlobalSettings.currKeybind[i].keyCodeValue.ToString();

            keybindScript.numOfControl[i] = keybindScript.col1.transform.GetChild(i).GetComponent<Text>().text;
            keybindScript.numOfInput[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), keybindScript.col1.transform.GetChild(i).GetChild(1).GetComponent<Text>().text);

        }
        for (int i = 0; i < controlUICount2; i++)
        {
            keybindScript.col2.transform.GetChild(i).GetComponent<Text>().text = GlobalSettings.currKeybind[i+ controlUICount].controlName;
            keybindScript.col2.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = GlobalSettings.currKeybind[i+ controlUICount].keyCodeValue.ToString();

            keybindScript.numOfControl[i + controlUICount] = keybindScript.col2.transform.GetChild(i).GetComponent<Text>().text;
            keybindScript.numOfInput[i + controlUICount] = (KeyCode)System.Enum.Parse(typeof(KeyCode), keybindScript.col2.transform.GetChild(i).GetChild(1).GetComponent<Text>().text);
        }

    }

    public void UpdateTheKeybind() //Update script to UI
    {
        keybindScript = GetComponent<KeybindController>();

        controlUICount = keybindScript.col1.transform.childCount;
        controlUICount2 = keybindScript.col2.transform.childCount;
        for (int i = 0; i < controlUICount; i++)
        {
            keybindScript.col1.transform.GetChild(i).GetComponent<Text>().text = keybindScript.numOfControl[i];
            keybindScript.col1.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = keybindScript.numOfInput[i].ToString();
        }
        for (int i = 0; i < controlUICount2; i++)
        {
            keybindScript.col2.transform.GetChild(i).GetComponent<Text>().text = keybindScript.numOfControl[i + controlUICount];
            keybindScript.col2.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = keybindScript.numOfInput[i + controlUICount].ToString();
        }
        Debug.Log("Keybind UI is Updated!");
    }

    public void SaveTheKeybind() //Update UI to Game & Script
    {
        #region Old Code
        //KeybindController.keyBinds.Clear();
        //for (int i = 0; i < keybindScript.numOfControl.Length; i++)
        //{
        //    if (keybindScript.numOfControl[i] == "") continue;
        //    KeybindController.keyBinds.Add(keybindScript.numOfControl[i], keybindScript.numOfInput[i]);

        //}
        #endregion
        keybindScript = GetComponent<KeybindController>();

        if (savingKeybind == null || savingKeybind.Length != keybindScript.numOfControl.Length)
        {
            savingKeybind = new KeybindClass[keybindScript.numOfControl.Length]; 
        }
        if (savingKeybind[0] == null)
        {
            for (int i = 0; i < savingKeybind.Length; i++)
            {
                savingKeybind[i] = new KeybindClass();
                savingKeybind[i].controlName = "null";
                savingKeybind[i].keyCodeValue = KeyCode.None;
            }
        }
        else
        {
            Debug.Log("savingKeybind exists");
        }

        controlUICount = keybindScript.col1.transform.childCount;
        controlUICount2 = keybindScript.col2.transform.childCount;

        for (int i = 0; i < controlUICount; i++)
        {
            savingKeybind[i].controlName = keybindScript.col1.transform.GetChild(i).GetComponent<Text>().text;
            savingKeybind[i].keyCodeValue = (KeyCode)System.Enum.Parse(typeof(KeyCode), keybindScript.col1.transform.GetChild(i).GetChild(1).GetComponent<Text>().text);
        }
        for (int i = 0; i < controlUICount2; i++)
        {
            savingKeybind[i + controlUICount].controlName = keybindScript.col2.transform.GetChild(i).GetComponent<Text>().text;
            savingKeybind[i + controlUICount].keyCodeValue = (KeyCode)System.Enum.Parse(typeof(KeyCode), keybindScript.col2.transform.GetChild(i).GetChild(1).GetComponent<Text>().text);
        }

        //KeybindClass fuckyou = new KeybindClass();
        //fuckyou.controlName = "lick up";
        //fuckyou.keyCodeValue = KeyCode.A;
        string rPath = Application.dataPath + "/Resources/";
        XMLOp.Serialize(savingKeybind, rPath + "Keybind_Save.xml");
        GlobalSettings.SetupKeybind();

        Debug.Log("Keybind XML is Updated!");
   

    }
}
