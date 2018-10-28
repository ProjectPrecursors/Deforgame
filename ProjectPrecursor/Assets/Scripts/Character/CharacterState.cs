using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class CharacterState : MonoBehaviour {

    public enum priCharState { stand, walking, running, crouching, jumping, inair, stunned, dead } //primary states, must be one of this at all times
    public enum secCharState { none, shooting, melee, reloading, interacting } //Can be combined with primary state
    public priCharState currPriState = priCharState.inair;
    public secCharState currSecState = secCharState.none;
    public GameObject PlayerUI;




    /// AssetDatabase. Load Asset At Path ("Asset/Scripts/Equipment/Equipment_Default.asset",typeof(EquipmentClass)) as EquipmentClass;

    [SerializeField]
    public BodypartClass[] characterBody = new BodypartClass[7]
    {
        new BodypartClass(BodypartClass.bodyPartsSlot.head, 1, BodypartClass.healthStatus.healthy, 1.0f, null, new List<EquipmentClass>() ),
        new BodypartClass(BodypartClass.bodyPartsSlot.upBody, 1, BodypartClass.healthStatus.healthy, 1.0f, null, new List<EquipmentClass>() ),
        new BodypartClass(BodypartClass.bodyPartsSlot.lowBody, 1, BodypartClass.healthStatus.healthy, 1.0f,  null,new List<EquipmentClass>() ),
        new BodypartClass(BodypartClass.bodyPartsSlot.rightarm, 1, BodypartClass.healthStatus.healthy, 1.0f,  null, new List<EquipmentClass>()),
        new BodypartClass(BodypartClass.bodyPartsSlot.leftarm, 1, BodypartClass.healthStatus.healthy, 1.0f,  null, new List<EquipmentClass>() ),
        new BodypartClass(BodypartClass.bodyPartsSlot.rightleg, 1, BodypartClass.healthStatus.healthy, 1.0f, null, new List<EquipmentClass>()),
        new BodypartClass(BodypartClass.bodyPartsSlot.leftleg, 1, BodypartClass.healthStatus.healthy, 1.0f,  null, new List<EquipmentClass>() )
    };

    [SerializeField]
    public BodypartClass inventoryContainer = new BodypartClass(BodypartClass.bodyPartsSlot.inventory, 3, BodypartClass.healthStatus.healthy, 1.0f, null, new List<EquipmentClass>());

    

    // Use this for initialization
    void Start () {
    }
	

	// Update is called once per frame
	void Update () {
        
        
    }

    public void HealthChange(float change, BodypartClass bodyP)
    {
        bodyP.currHealth += change;
        PlayerUI.GetComponentInChildren<HealthSystem>().HPBarUpdate(System.Array.IndexOf(characterBody, bodyP), bodyP.currHealth);

        CustomDeLogger.DLog_Health("Health Change For " + bodyP.bodySlot.ToString() + " to <color=red> " + bodyP.currHealth + "</color>");
    }
}
