using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbillity : MonoBehaviour {

    public EquipmentClass equipedMelee;
    public EquipmentClass equipedRange;
    public EquipmentClass equipedSpecialActive;
    public EquipmentClass equipedSpecialPassive;
    private List<string> emptyInputSlots;

    public GameObject playerDamager;
    private CharacterState CharStateScript;
    private CharacterMovement CharMoveScript;

    [Space]

    public InventorySystem inventorySysScript;

    private List<EquipmentClass> cooldownList = new List<EquipmentClass>();
    private List<EquipmentClass> resumeCDlist = new List<EquipmentClass>();
    private bool anyResumeCD = false;



    // Use this for initialization
    void Start() {

        CharStateScript = GetComponent<CharacterState>();
        CharMoveScript = GetComponent<CharacterMovement>();
        AssignEquipAllRandom(true); //Automatically slot first avaiable weapon to each slot
        if (inventorySysScript != null)
        {
            inventorySysScript.Init();
            inventorySysScript.StocktakingEquipped(new EquipmentClass[] { equipedMelee, equipedRange, equipedSpecialActive, equipedSpecialPassive });
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(GlobalSettings.keyBinds["Melee"]))
        {
            Debug.Log("Melee");
            if (equipedMelee.cooldownLeft == 0)
            {
                equipedMelee.Usage(gameObject, playerDamager);
            }
            

            if (equipedMelee.cooldownLeft > 0) cooldownList.Add(equipedMelee);
        }
        else if (Input.GetKeyDown(GlobalSettings.keyBinds["Range"]))
        {
            if (equipedRange.cooldownLeft == 0)
            {
                equipedRange.Usage(gameObject, null);
            }

            if (equipedRange.cooldownLeft > 0) cooldownList.Add(equipedRange);
        }


        if (cooldownList.Count >0) UpdateEquipmentCooldown();
    }

    private void AssignEquip(EquipmentClass equipInputSlot, EquipmentClass newEquipment) {
        equipInputSlot = newEquipment;
    }

    private void UnassignEquip(EquipmentClass equipInputSlot, EquipmentClass currEquipment)
    {
        if (equipInputSlot == currEquipment)
        {
            equipInputSlot = GlobalSettings.DEFAULTEQUIPMENT;
            emptyInputSlots.Add(equipInputSlot.ToString());
        }
    }

    private void UnassignEquipAll()
    {
        equipedMelee = GlobalSettings.DEFAULTEQUIPMENT;
        equipedRange = GlobalSettings.DEFAULTEQUIPMENT;
        equipedSpecialActive = GlobalSettings.DEFAULTEQUIPMENT;
        equipedSpecialPassive = GlobalSettings.DEFAULTEQUIPMENT;
        emptyInputSlots = new List<string> { "equipedMelee", "equipedRange", "equipedSpecialActive", "equipedSpecialPassive" };
    }



    public void AssignEquipAllRandom(bool reset)
    {
        if (reset)
            UnassignEquipAll();
        emptyInputSlots = new List<string> { "equipedMelee", "equipedRange", "equipedSpecialActive", "equipedSpecialPassive" };

        string equipFound = "";
        for (int i = 0; i < CharStateScript.characterBody.Length; i++)
        {
            if (emptyInputSlots.Count == 0) break;

            foreach (string e in emptyInputSlots)
            {
                switch (e)
                {
                    case "equipedMelee":
                        if (CharStateScript.characterBody[i].priSlot.behaviourType == EquipmentClass.eqpBehaviourType.melee)
                        {
                            equipedMelee = CharStateScript.characterBody[i].priSlot;
                            equipedMelee.currStatus = EquipmentClass.equipmentStatus.equipped;
                            equipFound = e;
                            break;
                        }
                        else
                        {
                            foreach (EquipmentClass a in CharStateScript.characterBody[i].secSlot)
                            {
                                if (a.behaviourType == EquipmentClass.eqpBehaviourType.melee)
                                {
                                    equipedMelee = a;
                                    equipedMelee.currStatus = EquipmentClass.equipmentStatus.equipped;
                                    equipFound = e;
                                    break;
                                }
                            }
                        }
                        break;

                    case "equipedRange":
                        if (CharStateScript.characterBody[i].priSlot.behaviourType == EquipmentClass.eqpBehaviourType.range)
                        {
                            equipedRange = CharStateScript.characterBody[i].priSlot;
                            equipedRange.currStatus = EquipmentClass.equipmentStatus.equipped;
                            equipFound = e;
                            break;
                        }
                        else
                        {
                            foreach (EquipmentClass a in CharStateScript.characterBody[i].secSlot)
                            {
                                if (a.behaviourType == EquipmentClass.eqpBehaviourType.range)
                                {
                                    equipedRange = a;
                                    equipedRange.currStatus = EquipmentClass.equipmentStatus.equipped;
                                    equipFound = e;
                                    break;
                                }
                            }
                        }
                        break;

                    case "equipedSpecialActive":
                        if (CharStateScript.characterBody[i].priSlot.behaviourType == EquipmentClass.eqpBehaviourType.specialA)
                        {
                            equipedSpecialActive = CharStateScript.characterBody[i].priSlot;
                            equipedSpecialActive.currStatus = EquipmentClass.equipmentStatus.equipped;
                            equipFound = e;
                            break;
                        }
                        else
                        {
                            foreach (EquipmentClass a in CharStateScript.characterBody[i].secSlot)
                            {
                                if (a.behaviourType == EquipmentClass.eqpBehaviourType.specialA)
                                {
                                    equipedSpecialActive = a;
                                    equipedSpecialActive.currStatus = EquipmentClass.equipmentStatus.equipped;
                                    equipFound = e;
                                    break;
                                }
                            }
                        }
                        break;

                    case "equipedSpecialPassive":
                        if (CharStateScript.characterBody[i].priSlot.behaviourType == EquipmentClass.eqpBehaviourType.specialP)
                        {
                            equipedSpecialPassive = CharStateScript.characterBody[i].priSlot;
                            equipedSpecialPassive.currStatus = EquipmentClass.equipmentStatus.equipped;
                            equipFound = e;
                            break;
                        }
                        else
                        {
                            foreach (EquipmentClass a in CharStateScript.characterBody[i].secSlot)
                            {
                                if (a.behaviourType == EquipmentClass.eqpBehaviourType.specialP)
                                {
                                    equipedSpecialPassive = a;
                                    equipedSpecialPassive.currStatus = EquipmentClass.equipmentStatus.equipped;
                                    equipFound = e;
                                    break;
                                }
                            }
                        }
                        break;
                }
                //End Of string loop
            }
            if (equipFound != "")
            {
                emptyInputSlots.Remove(equipFound);
                equipFound = "";
                continue;
            }
            //End of equipment loop
        }
        //END of AssignEquipAllRandom()
    }

    private void UpdateEquipmentCooldown()
    {
        anyResumeCD = false;

        foreach (EquipmentClass equipObj in cooldownList)
        {   
            equipObj.cooldownLeft -= Time.deltaTime;
            if (equipObj.cooldownLeft <= 0)
            {
                equipObj.cooldownLeft = 0;
                resumeCDlist.Add(equipObj);
                anyResumeCD = true;
            }
        }

        if (!anyResumeCD) return;
        foreach (EquipmentClass equipObj2 in resumeCDlist)
        {
            cooldownList.Remove(equipObj2);
        }
        resumeCDlist.Clear();
        anyResumeCD = false;
    }


}
