using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="New Equipment", menuName ="Equipment/Default")]
public class EquipmentClass:ScriptableObject {

    public string eName;
    public BodypartClass.bodyPartsSlot equipSlot;

    public enum equipmentStatus { equipped, backpack, destroyed, dropped, unknown};
    public equipmentStatus currStatus;
    public enum equipmentType { human, alien, wearable, consumable, none}
    public equipmentType isType;
    
    public int ammoCount; //999 for infinity
    public float cooldownCount;
    public enum eqpBehaviourType { none, range, melee, specialA, specialP, singleStraight, multiStraight, singleTrajectory ,dash}
    public eqpBehaviourType behaviourType = eqpBehaviourType.none;
    public eqpBehaviourType subbehaviourType = eqpBehaviourType.none;

    public Sprite sprite;
    public AnimationClip fuckme;

    [HideInInspector] public float cooldownLeft = 0;

    //123 public EquipmentClass(String n, BodypartClass.bodyPartsSlot slot, equipmentStatus cStats, equipmentType eType, int ammo, float cdTime)
    //{
    //    eName = n;
    //    equipSlot = slot;
    //    currStatus = cStats;
    //    isType = eType;
    //    ammoCount = ammo;
    //    cooldownCount = cdTime;
    //}
    //new EquipmentClass("Default",BodypartClass.bodyPartsSlot.none, EquipmentClass.equipmentStatus.unknown, EquipmentClass.equipmentType.none, 0,0f)


    public virtual void Init() { } //Used to set the values

    public virtual void Usage(GameObject origin, GameObject damagingObj) {

    }

    public virtual void TempUsage(GameObject origin, GameObject damagingObj)
    {

    }


}
