using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEquipment : MonoBehaviour {

    public EquipmentClass whichEquipment;
    public InventorySystem playerInventory;

    public void Start()
    {
        whichEquipment.currStatus = EquipmentClass.equipmentStatus.dropped;
    }
}
