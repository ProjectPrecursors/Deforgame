using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GlobalSettings : MonoBehaviour {

    public static GlobalSettings instance = null;

    public static KeybindClass[] currKeybind;
    public static Dictionary<string, KeyCode> keyBinds = new Dictionary<string, KeyCode>();
    public static bool iskeybindAssign = false;
    
    public static EquipmentClass DEFAULTEQUIPMENT;

    // Use this for initialization
    void Awake () {
        //Check if instance already exists, 
        if (instance == null)
            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        Init();
    }

    private void Init()
    {
        if (currKeybind == null) SetupKeybind();
        DEFAULTEQUIPMENT = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Equipment/Equipment_Default.asset", typeof(EquipmentClass)) as EquipmentClass;
    }

    public static void SetupKeybind()
    {
        currKeybind = XMLOp.Deserialize<KeybindClass[]>(Application.dataPath + "/Resources/" + "Keybind_Save.xml");

        keyBinds.Clear();
        for (int i = 0; i < currKeybind.Length; i++)
        {
            if (currKeybind[i].controlName == "") continue;
            keyBinds.Add(currKeybind[i].controlName, currKeybind[i].keyCodeValue);

        }

        iskeybindAssign = true;
    }
}
