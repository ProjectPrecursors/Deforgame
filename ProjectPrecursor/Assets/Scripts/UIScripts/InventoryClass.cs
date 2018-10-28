using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryClass {

    public int slotIndex;

    public bool isEmpty;
    public bool isEquipped;
    public bool isSelected;
    public bool isMoving;

    public EquipmentClass slotContent;
    public string contentName;

    public InventoryClass()
    {
        slotIndex = 0;

        isEmpty = true;
        isEquipped = false;
        isSelected = false;
        isMoving = false;

        slotContent = null;
        contentName = "";
}

    public InventoryClass(int Index, bool empty, bool equiped, bool select, bool moving, EquipmentClass content, string contntName)
    {
        slotIndex = Index;

        isEmpty = empty;
        isEquipped = equiped;
        isSelected = select;
        isMoving = moving;

        slotContent = content;
        contentName = contntName;
    }

}
