using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour {

    public GameObject headHealth;
    public GameObject upBodyHealth;
    public GameObject lowBodyHealth;
    public GameObject rightarmHealth;
    public GameObject leftarmHealth;
    public GameObject rightlegHealth;
    public GameObject leftlegHealth;

    public List<GameObject> bodyPartsList ;
 

    // Use this for initialization
    void Start () {
        bodyPartsList = new List<GameObject>{
            headHealth,
            upBodyHealth,
            lowBodyHealth,
            rightarmHealth,
            leftarmHealth,
            rightlegHealth,
            leftlegHealth
        };

    }
	
	// Update is called once per frame
	public void HPBarUpdate(int index, float currHP) {
        bodyPartsList[index].GetComponent<Image>().fillAmount = currHP;
        Debug.Log(bodyPartsList[index].GetComponent<Image>().fillAmount);

    }
}
