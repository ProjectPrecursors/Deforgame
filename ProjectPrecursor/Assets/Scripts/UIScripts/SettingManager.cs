using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour {

    public GameObject controlPg;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void SaveAllChanges () {
        controlPg.GetComponent<UpdateKeybind>().SaveTheKeybind();

    }
}
