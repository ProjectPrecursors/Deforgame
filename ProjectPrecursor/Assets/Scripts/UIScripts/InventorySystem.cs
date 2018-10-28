using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour {

    /// <summary>
    /// Really need to clean up and improve
    /// </summary>


    public GameObject inventoryParent;
    public GameObject playerObj;
    public GameObject PickUpPrefab;

    private CharacterState playerCharStateScript;
    public int tempInventoryCount;
    public List<InventoryClass> inventoryList;
    public int selectingIndex = 0;

    public Color equippedColor;
    public Color normalColor;

    private GameObject inventoryUI;
    private Vector3 inventoryUIOrigPos;

    private float lastInteractedTime;
    public Vector2 activeHideY;
    public bool isHidden = false;


    public void Start()
    {
        inventoryList = new List<InventoryClass>(tempInventoryCount);
    }

    public void Init()
    {
        for (int i = 0; i < tempInventoryCount; i++)
        {
            inventoryList.Add(new InventoryClass(i,true, false, false, false, null, ""));
        }
        inventoryUIOrigPos = inventoryParent.transform.GetChild(0).gameObject.GetComponent<Image>().rectTransform.position;
        MoveSelection(0);
    }

    public void StocktakingEquipped(EquipmentClass[] newEquipIn)
    {
        ClearAllEquipped();
        foreach (EquipmentClass equipIn in newEquipIn)
        {
            if (equipIn != null && equipIn != GlobalSettings.DEFAULTEQUIPMENT)
            {
                Debug.Log(FindEmptySlot() +"   "+ equipIn);
                EquipIntoSlot(FindEmptySlot(), equipIn, true);
            }
            
        }
    }

    private void ClearAllEquipped()
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            inventoryUI = inventoryParent.transform.GetChild(i).gameObject;
            if (inventoryList[i].isEquipped)
            {
                inventoryUI.transform.GetChild(0).GetComponent<Text>().text = "";
                inventoryUI.GetComponent<Image>().color = normalColor;
                inventoryList[i].isEmpty = true;
                inventoryList[i].isEquipped = false;
                inventoryList[i].slotContent = null;
                inventoryList[i].contentName = "";
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(GlobalSettings.keyBinds["Inventory Right"])){
            MoveSelection(1);
            lastInteractedTime = Time.time;
        }
        else if (Input.GetKeyDown(GlobalSettings.keyBinds["Inventory Left"])){
            MoveSelection(-1);
            lastInteractedTime = Time.time;
        }
        else if (Input.GetKeyDown(GlobalSettings.keyBinds["Equip Selected"]))
        {
            SwitchEquipment(inventoryList[selectingIndex].slotContent, selectingIndex);
            lastInteractedTime = Time.time;
        }
        else if (Input.GetKeyDown(GlobalSettings.keyBinds["Drop Selected"]))
        {
            DropEquipment(inventoryList[selectingIndex]);
            lastInteractedTime = Time.time;
        }



        if (inventoryList.Count == 0) return;

        for (int i = 0; i < inventoryList.Count; i++)
        {
            inventoryUI = inventoryParent.transform.GetChild(i).gameObject;

            if (selectingIndex == i)
            {
                
            }

        }

        ///HIDE UI
        if (!isHidden && Time.time - lastInteractedTime >= 3)
        {
            inventoryParent.GetComponent<Image>().color = new Color(inventoryParent.GetComponent<Image>().color.r, inventoryParent.GetComponent<Image>().color.g, inventoryParent.GetComponent<Image>().color.b, 0f);
            for (int i = 0; i < inventoryList.Count; i++)
            {
                inventoryUI = inventoryParent.transform.GetChild(i).gameObject;
                inventoryUI.GetComponent<Image>().color = new Color(inventoryUI.GetComponent<Image>().color.r, inventoryUI.GetComponent<Image>().color.g, inventoryUI.GetComponent<Image>().color.b, 0f);
                inventoryUI.transform.GetChild(0).gameObject.GetComponent<Text>().color = 
                    new Color(inventoryUI.transform.GetChild(0).gameObject.GetComponent<Text>().color.r, inventoryUI.transform.GetChild(0).gameObject.GetComponent<Text>().color.g, inventoryUI.transform.GetChild(0).gameObject.GetComponent<Text>().color.b, 0f);

            }
            isHidden = true;
        }
        else if (isHidden && Time.time - lastInteractedTime <= 1)
        {
            inventoryParent.GetComponent<Image>().color = new Color(inventoryParent.GetComponent<Image>().color.r, inventoryParent.GetComponent<Image>().color.g, inventoryParent.GetComponent<Image>().color.b, 100/255f);
            for (int i = 0; i < inventoryList.Count; i++)
            {
                inventoryUI = inventoryParent.transform.GetChild(i).gameObject;
                inventoryUI.GetComponent<Image>().color = new Color(inventoryUI.GetComponent<Image>().color.r, inventoryUI.GetComponent<Image>().color.g, inventoryUI.GetComponent<Image>().color.b, 1f);
                inventoryUI.transform.GetChild(0).gameObject.GetComponent<Text>().color =
                    new Color(inventoryUI.transform.GetChild(0).gameObject.GetComponent<Text>().color.r, inventoryUI.transform.GetChild(0).gameObject.GetComponent<Text>().color.g, inventoryUI.transform.GetChild(0).gameObject.GetComponent<Text>().color.b, 1f);
            }
            isHidden = false;
        }

	}

    private void EquipIntoSlot(int index, EquipmentClass content ,bool isequippedItem)
    {
        //Assumed its empty, however will do a check
        if (inventoryList[index].isEmpty)
        {
            inventoryUI = inventoryParent.transform.GetChild(index).gameObject;
            inventoryList[index].isEmpty = false;
            inventoryList[index].isEquipped = isequippedItem;
            inventoryList[index].slotContent = content;
            inventoryList[index].contentName = content.eName;

            inventoryUI.transform.GetChild(0).GetComponent<Text>().text = content.eName;

            inventoryUI.GetComponent<Image>().color = isequippedItem? equippedColor:normalColor;

            if (isequippedItem)
            {   
                if (content.currStatus == EquipmentClass.equipmentStatus.dropped)
                {
                    int pickupIndex = BodypartClass.FindBodyPartIndex(content.equipSlot);
                    int secSlotIndex = -1;
                    for (int e = 0; e < playerObj.GetComponent<CharacterState>().characterBody[pickupIndex].secSlot.Count; e++)
                    {
                        if (playerObj.GetComponent<CharacterState>().characterBody[pickupIndex].secSlot[e] == GlobalSettings.DEFAULTEQUIPMENT)
                        {
                            secSlotIndex = e;
                        }
                    }
                    playerObj.GetComponent<CharacterState>().characterBody[pickupIndex].secSlot[secSlotIndex] = content;
                    playerObj.GetComponent<CharacterState>().characterBody[pickupIndex].secSlot[secSlotIndex].currStatus = EquipmentClass.equipmentStatus.equipped;
                }
                
                
                //WILL NEED TO CODE ADD BACK TO PLAYER BODY PART
                playerObj.GetComponent<CharacterAbillity>().AssignEquipAllRandom(true);
            }
        }
        else
        {
            Debug.Log("ERROR< SLOT NOT EMPTY");  
            return;
        }
    }

    private void MoveSelection(int indexChange)
    {   
        Vector3 origPosXZ = inventoryParent.transform.GetChild(selectingIndex).gameObject.GetComponent<Image>().rectTransform.position;
        inventoryParent.transform.GetChild(selectingIndex).gameObject.GetComponent<Image>().rectTransform.position = new Vector3(origPosXZ.x, inventoryUIOrigPos.y, origPosXZ.z);
        if (selectingIndex + indexChange < 0 )
        {
            selectingIndex = tempInventoryCount - 1;
        }
        else if (selectingIndex + indexChange >= tempInventoryCount)
        {
            selectingIndex = 0;
        }
        else
        {
            selectingIndex += indexChange;
        }
        origPosXZ = inventoryParent.transform.GetChild(selectingIndex).gameObject.GetComponent<Image>().rectTransform.position;
        inventoryParent.transform.GetChild(selectingIndex).gameObject.GetComponent<Image>().rectTransform.position = new Vector3(origPosXZ.x, inventoryUIOrigPos.y +10f, origPosXZ.z);
    }

    private int FindEmptySlot()
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].isEmpty)
            {
                return i;
            }
        }
        return -1;
    }

    public bool EquipFromPickup(EquipmentClass newItem)
    {
        if (FindEmptySlot()!= -1)
        {
            //CheckPickUpAutoEquip() will determine if item can be auto equip into empty bodypart
            EquipIntoSlot(FindEmptySlot(), newItem, CheckPickupAutoEquip(newItem));
            
            return true;
        }
        return false;
    }

    public void SwitchEquipment(EquipmentClass switchNew, int selectedIndex)
    {
        if (inventoryList[selectedIndex].isEquipped) return;


        //First check if there is an exist equiped in slot
        playerCharStateScript = playerObj.GetComponent<CharacterState>();
        int newEqpmIndex = BodypartClass.FindBodyPartIndex(switchNew.equipSlot);
        if (playerCharStateScript.characterBody[newEqpmIndex].bodySlot == switchNew.equipSlot)
        {
            // Will need to overwrite the currently equiped with new one
            EquipmentClass oldEquip =  playerCharStateScript.characterBody[newEqpmIndex].secSlot[0];
            playerCharStateScript.characterBody[newEqpmIndex].secSlot[0] = switchNew;
            playerCharStateScript.characterBody[newEqpmIndex].secSlot[0].currStatus = EquipmentClass.equipmentStatus.equipped;
            
            playerObj.GetComponent<CharacterAbillity>().AssignEquipAllRandom(true);

            EquipInventory(switchNew);
            //Place the old equip into inventory
            UnequipInventory(oldEquip);
        }
    }

    public void DropEquipment(InventoryClass dropSlot)
    {
        if (dropSlot.isEmpty) return;
        //Check whether is an equiped item
        if (dropSlot.slotContent.currStatus == EquipmentClass.equipmentStatus.equipped)
        {
            playerCharStateScript = playerObj.GetComponent<CharacterState>();
            /////int dropEqpmIndex = System.Array.IndexOf(playerCharStateScript.characterBody, dropSlot.slotContent.equipSlot);

            int dropEqpmIndex = BodypartClass.FindBodyPartIndex(dropSlot.slotContent.equipSlot);


            if (dropEqpmIndex != -1) //Comfirm is an equipped item
            {
                
                if (playerCharStateScript.characterBody[dropEqpmIndex].priSlot == dropSlot.slotContent)
                {
                    playerCharStateScript.characterBody[dropEqpmIndex].priSlot = GlobalSettings.DEFAULTEQUIPMENT;

                }
                else if (playerCharStateScript.characterBody[dropEqpmIndex].secSlot.Contains(dropSlot.slotContent))
                {
                    int removeIndex = playerCharStateScript.characterBody[dropEqpmIndex].secSlot.IndexOf(dropSlot.slotContent);
                    playerCharStateScript.characterBody[dropEqpmIndex].secSlot[removeIndex] = GlobalSettings.DEFAULTEQUIPMENT;
                    Debug.Log(GlobalSettings.DEFAULTEQUIPMENT);
                }
            }
        }

        //Now Drop a weapon Pickup
        GameObject newPickUP = Instantiate(PickUpPrefab, playerObj.transform.position + Vector3.right*2f,Quaternion.identity);
        newPickUP.GetComponent<PickUpEquipment>().playerInventory = this;
        newPickUP.GetComponent<PickUpEquipment>().whichEquipment = dropSlot.slotContent;
        newPickUP.GetComponent<PickUpEquipment>().whichEquipment.currStatus = EquipmentClass.equipmentStatus.dropped;

        EmptySlot(dropSlot);
    }

    void EmptySlot(InventoryClass slot)
    {
        if (inventoryList.Contains(slot))
        {
            inventoryUI = inventoryParent.transform.GetChild(slot.slotIndex).gameObject;

            inventoryUI.transform.GetChild(0).GetComponent<Text>().text = "";
            inventoryUI.GetComponent<Image>().color = normalColor;
            inventoryList[slot.slotIndex].isEmpty = true;
            inventoryList[slot.slotIndex].isEquipped = false;
            inventoryList[slot.slotIndex].slotContent = null;
            inventoryList[slot.slotIndex].contentName = "";

            playerObj.GetComponent<CharacterAbillity>().AssignEquipAllRandom(true);
        }
    }

    bool CheckPickupAutoEquip(EquipmentClass eqpm)
    {
        BodypartClass.bodyPartsSlot pickupBodyPart = eqpm.equipSlot;
        int pickupIndex = BodypartClass.FindBodyPartIndex(eqpm.equipSlot);

        foreach (EquipmentClass e in playerObj.GetComponent<CharacterState>().characterBody[pickupIndex].secSlot)
        {
            if (e == GlobalSettings.DEFAULTEQUIPMENT)
            {
                return true;
            }
        }
        return false;
    }

    void UnequipInventory(EquipmentClass eqpm)
    {
        int index = SearchInventory(eqpm);

        inventoryUI = inventoryParent.transform.GetChild(index).gameObject;
        inventoryList[index].isEquipped = false;
        inventoryUI.GetComponent<Image>().color = normalColor;
    }

    int SearchInventory(EquipmentClass eqpm)
    {
        for (int i = 0; i <inventoryList.Count; i++)
        {
            if (inventoryList[i].slotContent == eqpm)
            {
                return i;
            }
        }
        return -1;
    }

    void EquipInventory(EquipmentClass eqpm)
    {
        int index = SearchInventory(eqpm);

        inventoryUI = inventoryParent.transform.GetChild(index).gameObject;
        inventoryList[index].isEquipped = true;
        inventoryUI.GetComponent<Image>().color = equippedColor;
    }
}
