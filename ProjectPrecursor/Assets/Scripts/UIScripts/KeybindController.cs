using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindController : MonoBehaviour {

    
    private KeyCode newKeypressed = KeyCode.None;
    public string[] numOfControl;
    public KeyCode[] numOfInput;
    public GameObject col1, col2;
    public bool isModifying = false;
    public bool isModifyingP2 = false;
    private GameObject modifiedObj;

    // Use this for initialization
    void Start () {
        if (GlobalSettings.currKeybind != null)
        {
            GetComponent<UpdateKeybind>().LoadTheKeybind();
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (newKeypressed != KeyCode.None && isModifying)
        {
            modifiedObj.GetComponent<Text>().text = newKeypressed.ToString();
            newKeypressed = KeyCode.None;
            modifiedObj = null;
            isModifying = false;
            isModifyingP2 = false;
        }

    }

    public void ModifyKeybind(GameObject data) //UP
    {
        if (!isModifyingP2) return;
        modifiedObj.GetComponent<Text>().text = "Press New Key";
        isModifying = true;
        
    }

    public void ModifyKeybindP2(GameObject data) //Down
    {
        modifiedObj = data;
        isModifyingP2 = true;
    }


    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && isModifying)
        {
            //Debug.Log("Keyboard" + e.keyCode);
            newKeypressed = e.keyCode;
        }
        else if (e.isMouse && isModifying)
        {
            Debug.Log("Mouse" + e.button);
            if (e.button == 0 )
            {
                newKeypressed = KeyCode.Mouse0;
            }
            else if (e.button == 1)
            {
                newKeypressed = KeyCode.Mouse1;
            }
            else if (e.button == 2)
            {
                newKeypressed = KeyCode.Mouse2;
            }

        }
    }

}
