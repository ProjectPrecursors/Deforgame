using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour {

    public List<string> damageLayer;

    private PickUpEquipment pickUpScript;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (damageLayer.Contains("Damager_Enemy") && col.gameObject.tag == "Damager_Enemy")
        {
            GetComponent<CharacterState>().HealthChange(-0.25f, GetComponent<CharacterState>().characterBody[Random.Range(0, 6)]);

            Destroy(col.gameObject);

        }
        else if (damageLayer.Contains("Damager_Player") && col.gameObject.tag == "Damager_Player")
        {
            Destroy(gameObject);
            Debug.Log("FINGL");
            Destroy(col.gameObject);

        }

        if (this.gameObject.tag == "Player" && col.gameObject.tag == "PickUp")
        {
            pickUpScript = col.gameObject.GetComponent<PickUpEquipment>();
            if (pickUpScript.playerInventory.EquipFromPickup(pickUpScript.whichEquipment))
            {
                Debug.Log("Successful" + pickUpScript.whichEquipment.currStatus);
                Destroy(col.gameObject);
            }
            else
            {
                Debug.Log("UNSuccessful");
            }
        }
    }
}
